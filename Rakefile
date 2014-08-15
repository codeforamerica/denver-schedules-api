require "centroid"

Config = Centroid::Config.from_file "config.json"

desc "Checks the local system"
task :ok? do
  Config.all.variables.each do |variable|
    message = "#{variable} environment variable is missing."
    raise message unless ENV.has_key? variable
  end
  puts "ok"
end
