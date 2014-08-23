require "centroid"
require "httparty"
require "json"
require "albacore"

Config = Centroid::Config.from_file "config.json"

ROOT = File.expand_path "."

namespace :build do
  desc "Build for debugging"
  build :debug do |b|
    b.sln = Config.all.build.solution
    b.prop "Configuration", "Debug"
    b.logging = "minimal"
  end

  desc "Build for release"
  build :release do |b|
    b.sln = Config.all.build.solution
    b.prop "Configuration", "Release"
    b.logging = "minimal"
  end
end

namespace :test do
  desc "Run system tests"
  test_runner :system do |tr|
    clean(Config.all.test.output)
    tr.exe = Config.all.tools.nunit
    tr.files = [Config.all.test.dll]
    tr.add_parameter "-include=System"
    tr.add_parameter "-xml=#{File.join(ROOT, Config.all.test.results)}"
  end

  desc "Run reminder tests"
  test_runner :reminders => ["build:debug"] do |tr|
    clean(Config.all.test.output)
    tr.exe = Config.all.tools.nunit
    tr.files = [Config.all.test.dll]
    tr.add_parameter "-include=Reminder"
    tr.add_parameter "-xml=#{File.join(ROOT, Config.all.test.results)}"
  end

  desc "Run unit tests"
  test_runner :units => ["build:debug"] do |tr|
    clean(Config.all.test.output)
    tr.exe = Config.all.tools.nunit
    tr.files = [Config.all.test.dll]
    tr.add_parameter "-exclude=System,Reminder"
    tr.add_parameter "-xml=#{File.join(ROOT, Config.all.test.results)}"
  end

  desc "Run all tests"
  test_runner :all => ["build:debug"] do |tr|
    clean(Config.all.test.output)
    tr.exe = Config.all.tools.nunit
    tr.files = [Config.all.test.dll]
    tr.add_parameter "-xml=#{File.join(ROOT, Config.all.test.results)}"
  end
end

task :default => "test:all"

def authenticate(url, prompt=false)
  if prompt
    puts "username:"
    username = STDIN.gets.chomp

    puts "password:"
    password = STDIN.gets.chomp
  else
    username = ENV['ADMIN_USERNAME']
    password = ENV['ADMIN_PASSWORD']
  end

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

def send_email_reminders_with_prompt_task(auth_url, url)
  desc "Send email reminders and prompt for creds(#{url})"
  task :send_email_reminders_with_prompt do
    send_reminders(auth_url, url, true)
  end
end

def send_email_reminders_task(auth_url, url)
  desc "Send email reminders (#{url})"
  task :send_email_reminders do
    send_reminders(auth_url, url)
  end
end

def send_sms_reminders_with_prompt_task(auth_url, url)
  desc "Send sms reminders and prompt for creds(#{url})"
  task :send_sms_reminders_with_prompt do
    send_reminders(auth_url, url, true)
  end
end

def send_sms_reminders_task(auth_url, url)
  desc "Send sms reminders (#{url})"
  task :send_sms_reminders do
    send_reminders(auth_url, url)
  end
end

def send_reminders(auth_url, url, prompt=false)
  puts url

  response = authenticate auth_url, prompt
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
    send_email_reminders_with_prompt_task env_config.urls.authenticate, env_config.urls.sendEmailReminders
    send_sms_reminders_task env_config.urls.authenticate, env_config.urls.sendSMSReminders
    send_sms_reminders_with_prompt_task env_config.urls.authenticate, env_config.urls.sendSMSReminders
  end
end

def clean(dir)
  FileUtils.rm_rf dir
  FileUtils.mkdir_p dir
end
