require "centroid"
require "httparty"
require "json"

Config = Centroid::Config.from_file "config.json"

desc "Checks the local system"
task :ok? do
  Config.all.variables.each do |variable|
    message = "#{variable} environment variable is missing."
    raise message unless ENV.has_key? variable
  end
  puts "ok"

desc "Send reminder emails"
task :send do
  token = authenticate
  oneMinute = 60
  options = {
    headers: {
      "User-Agent" => "test",
      "Accept" => "application/json",
      "Authorization" => "Token " + token
    },
    body: {
      remindOn: Date.today,
    },
    timeout: oneMinute
  }
  response = HTTParty.post("http://localhost:8080/reminders/email/send", options)
  puts response.code, response.body
end

def authenticate
  username = ENV["ADMIN_USERNAME"]
  password = ENV["ADMIN_PASSWORD"]
  options = {
    headers: {
      "User-Agent" => "test"
    },
    body: {
      username: username,
      password: password
    }
  }
  response = HTTParty.post("http://localhost:8080/authenticate", options)
  data = JSON.parse(response.body)
  return data["token"]
end
