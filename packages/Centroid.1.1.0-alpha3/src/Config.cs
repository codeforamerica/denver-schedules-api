using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Centroid
{
    public class Config : DynamicObject, IEnumerable
    {
        public Config(string json)
        {
            RawConfig = JObject.Parse(json);
            ValidateUniqueKeys();
        }

        public Config(dynamic config)
        {
            RawConfig = config;
        }

        public static Config FromFile(string fileName)
        {
            string json = System.IO.File.ReadAllText(fileName);
            return new Config(json);
        }

        public dynamic RawConfig { get; set; }

        public object this[int index]
        {
            get { return GetValue(index); }
            set { RawConfig[index] = value; }
        }

        public bool ContainsKey(string key)
        {
            return GetActualKey(key) != null;
        }

        public dynamic ForEnvironment(string environment)
        {
            var envConfig = GetContainer(environment);
            envConfig["environment"] = environment;
            var allConfig = GetContainer("all");

            if (allConfig == null)
            {
                return new Config(envConfig);
            }

            MergeInto(allConfig, envConfig);

            return new Config(allConfig);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (ContainsKey(binder.Name))
            {
                result = GetValue(binder.Name);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof (string))
            {
                result = ToString();
                return true;
            }

            try
            {
                result = Convert.ChangeType(RawConfig, binder.Type);
                return true;
            }
            catch (InvalidCastException)
            {
                return base.TryConvert(binder, out result);
            }
        }

        public override string ToString()
        {
            return RawConfig.ToString(Formatting.None);
        }

        public IEnumerator GetEnumerator()
        {
            if (RawConfig is JArray)
            {
                foreach (var element in RawConfig)
                {
                    yield return GetValueFromContainer(element);
                }
            }
            else
            {
                foreach (var name in GetDynamicMemberNames())
                {
                    dynamic value = GetValue(name);
                    yield return new KeyValuePair<string,dynamic>(name, value);
                }
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            var container = (JObject) RawConfig;
            return container.Properties().Select(p => p.Name);
        }

        static string NormaliseKey(string key)
        {
            return key.Replace("_", String.Empty).ToLower();
        }

        static dynamic GetValueFromContainer(dynamic container)
        {
            if (container is JContainer)
            {
                return new Config(container);
            }
            return container.Value;
        }

        dynamic GetValue(int index)
        {
            var container = RawConfig[index];
            return GetValueFromContainer(container);
        }

        dynamic GetValue(string key)
        {
            var container = GetContainer(key);
            return GetValueFromContainer(container);
        }

        dynamic GetContainer(string key)
        {
            var actualKey = GetActualKey(key);
            return actualKey == null ? null : RawConfig[actualKey];
        }

        string GetActualKey(string key)
        {
            return GetDynamicMemberNames().SingleOrDefault(m => NormaliseKey(m) == NormaliseKey(key));
        }

        void ValidateUniqueKeys()
        {
            var normalizedKeys = GetDynamicMemberNames().Select(p => new { Key = p, NormalizedKey = NormaliseKey(p) });
            var duplicates = normalizedKeys.GroupBy(nk => nk.NormalizedKey).Where(g => g.Count() > 1).ToArray();

            if (!duplicates.Any()) return;

            var keys = duplicates.SelectMany(d => d.Select(x => x.Key));
            throw new InvalidOperationException("Centroid.Config instance contains duplicate keys: " + string.Join(", ", keys));
        }

        static void MergeInto(JContainer left, JToken right)
        {
            foreach (var rightChild in right.Children<JProperty>())
            {
                var rightChildProperty = rightChild;
                var leftProperty = left.SelectToken(rightChildProperty.Name);

                if (leftProperty == null)
                {
                    left.Add(rightChild);
                }
                else
                {
                    var leftObject = leftProperty as JObject;

                    if (leftObject == null)
                    {
                        var leftParent = (JProperty) leftProperty.Parent;
                        leftParent.Value = rightChildProperty.Value;
                    }
                    else
                    {
                        MergeInto(leftObject, rightChildProperty.Value);
                    }
                }
            }
        }
    }
}
