require "centroid"
require "httparty"
require "json"

Config = Centroid::Config.from_file "config.json"

namespace :test do
  desc "Run system tests"
  task :system do
    Config.all.variables.each do |variable|
      message = "#{variable} environment variable is missing."
      raise message unless ENV.has_key? variable
    end
    puts "All environment variables were found."
  end
end

def authenticate(url)
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
  response = HTTParty.post(url, options)
end

def authenticate_task(url)
  desc "Authenticate (#{url})"
  task :authenticate do
    puts url

    response = authenticate url
    puts response.code, response.body
  end
end

def create_email_reminder_task(url)
  desc "Create email reminder (#{url})"
  task :create_email_reminder do
    create_reminder(url)
  end
end

def create_sms_reminder_task(url)
  desc "Create sms reminder (#{url})"
  task :create_sms_reminder do
    create_reminder(url)
  end
end

def create_reminder(url)
  puts url

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

  response = HTTParty.post(url, options)
  puts response.code, response.body
end

def send_email_reminders_task(auth_url, url)
  desc "Send email reminders (#{url})"
  task :send_email_reminders do
    send_reminders(auth_url, url)
  end
end

def send_sms_reminders_task(auth_url, url)
  desc "Send sms reminders (#{url})"
  task :send_sms_reminders do
    send_reminders(auth_url, url)
  end
end

def send_reminders(auth_url, url)
  puts url

  response = authenticate auth_url
  data = JSON.parse(response.body)
  token = data["token"]

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
  response = HTTParty.post(url, options)
  puts response.code, response.body
end

Config.each do |environment|
  next if environment == "all"
  env_config = Config.for_environment environment

  namespace environment do
    authenticate_task env_config.urls.authenticate
    create_email_reminder_task env_config.urls.createEmailReminder
    create_sms_reminder_task env_config.urls.createSMSReminder
    send_email_reminders_task env_config.urls.authenticate, env_config.urls.sendEmailReminders
    send_sms_reminders_task env_config.urls.authenticate, env_config.urls.sendSMSReminders
  end
end
