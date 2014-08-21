require "centroid"
require "httparty"
require "json"

Config = Centroid::Config.from_file "config.json"

desc "Checks the local system"
task :ok? do
  Config.variables.each do |variable|
    message = "#{variable} environment variable is missing."
    raise message unless ENV.has_key? variable
  end
  puts "ok"
end

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
  response = HTTParty.post(Config.urls.sendEmailReminders, options)
  puts response.code, response.body
end

desc "Create reminder"
task :create_reminder do
  puts "contact:"
  contact = STDIN.gets.chomp

  options = {
    headers: {
      "User-Agent" => "test"
    },
    body: {
      contact: contact,
      message: "This is a test message. Cross your fingers...",
      remindOn: Date.today
    }
  }

  response = HTTParty.post(Config.urls.createReminders, options)
  puts response.code, response.body
end

def authenticate
  puts "username:"
  username = STDIN.gets.chomp

  puts "password:"
  password = STDIN.gets.chomp

  options = {
    headers: {
      "User-Agent" => "test"
    },
    body: {
      username: username,
      password: password
    }
  }

  response = HTTParty.post(Config.urls.authenticate, options)
  data = JSON.parse(response.body)
  return data["token"]
end
