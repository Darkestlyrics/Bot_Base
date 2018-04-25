using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using SoggyBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoggyBot.Classes {
    class Bot_Base {
        #region Properties

        public List<DiscordUser> Users { get; set; }

        public List<DiscordChannel> Channels { get; set; }

        public List<DiscordEmoji> Emojis { get; set; }

        public DiscordClient Client { get; set; }

        public string ServerName { get; set; }

        public bool Connected { get; set; }

        public CommandsNextModule Commands { get; set; }

        #endregion

        #region Constructor

        public Bot_Base(string serverName, DiscordConfiguration dsConfig, bool autostart = false) {
            ServerName = serverName;
            Client = new DiscordClient(dsConfig);
            var ccfg = new CommandsNextConfiguration {
                StringPrefix = ";;",
                EnableDms = true,
                EnableMentionPrefix = true
            };
            Commands = Client.UseCommandsNext(ccfg);
            Commands.RegisterCommands<CommandHolder>();
        }

        #endregion

        #region Methods

        #region Public
        public override string ToString() => SettingsHelper.GetValue("Name");

        public MethodResult Init() {
            try {
                init();
                return new MethodResult(true, null);
            } catch (Exception ex) {
                return new MethodResult(false, ex);
            }
        }

        public void GetUsers() {
            getUsers();
        }

        public void GetChannels() {
            getChannels();
        }

        public void GetEmojis() {
            getEmojis();
        }

        public MethodResult FindUser(string name) {
            try {
                return new MethodResult(true, findUser(name));
            } catch (Exception ex) {
                return new MethodResult(false, ex);
            }
        }

        public MethodResult FindChannel(string name) {
            try {
                return new MethodResult(true, findChannel(name));
            } catch (Exception ex) {
                return new MethodResult(false, ex);
            }
        }


        public void SetupEvents() {
            setupEvents();
        }

        public MethodResult Start(int RetryCount = 0) {
            try {
                start(RetryCount).GetAwaiter().GetResult();
                return new MethodResult(true, null);
            } catch (Exception ex) {
                return new MethodResult(false, ex);
            }
        }

        public MethodResult Stop() {
            try {
                stop().GetAwaiter().GetResult();
                return new MethodResult(true, null);
            } catch (Exception ex) {
                return new MethodResult(false, ex);
            }
        }

        public MethodResult SendMessage(string channel, string message) {
            try {
                DiscordChannel chan = findChannel(channel);
                sendMessage(chan, message);
                return new MethodResult(true, null);
            } catch (Exception ex) {
                return new MethodResult(false, ex);
            }
        }
        public MethodResult SendDM(string user, string message) {
            try {
                DiscordUser User = findUser(user);
                sendDM(User, message);
                return new MethodResult(true, null);
            } catch (Exception ex) {
                return new MethodResult(false, ex);
            }
        }

        #endregion

        #region Private

        private async Task start(int r = 0) {
            int i = 0;
            while (i != r && !Connected) {
                await Client.ConnectAsync();
                Thread.Sleep(10000);
                if (Client.Guilds.Count < 0)
                    Connected = false;
                else
                    Connected = true;
            }

        }

        private async Task stop() {
            await Client.DisconnectAsync();
        }

        private void init() {
            Connected = true;
            GetUsers();
            GetChannels();
            SetupEvents();
        }

        private void getUsers() {
            Users = Client.Guilds.First(gg => gg.Value.Name == ServerName).Value.Members.Where(uu => uu.IsBot == false && !uu.Roles.Any(role => role.Name.ToUpper() == "CORE")).ToList<DiscordUser>();
        }

        private void getChannels() {
            Channels = Client.Guilds.First(gg => gg.Value.Name == ServerName).Value.Channels.Where(cc => !cc.IsPrivate && cc.Type != ChannelType.Voice).ToList();
        }

        private void getEmojis() {
            Emojis = Client.Guilds.First(gg => gg.Value.Name == ServerName).Value.Emojis.ToList();
        }

        private DiscordEmoji findEmoji(string name) {
            return Emojis.Find(ee => ee.Name == name) ?? null;
        }

        private DiscordUser findUser(string name) {
            return Users.Find(uu => uu.Username == name) ?? null;
        }

        private DiscordChannel findChannel(string name) {
            return Channels.Find(cc => cc.Name == name) ?? null;
        }

        private void setupEvents() {
            Client.ClientErrored += _Client_ClientError;
             Client.MessageCreated += _Client_MessageCreated;
        }


        private async void sendMessage(DiscordChannel c, string s) {
            await Client.SendMessageAsync(c, s);
        }

        private async void sendDM(DiscordUser u, string s) {
            DiscordDmChannel c = await Client.CreateDmAsync(u);
            await Client.SendMessageAsync(c, s);
        }

        #endregion

        #endregion

        #region Events

        private async Task _Client_ClientError(ClientErrorEventArgs e) {
            AppState.Logger.WriteLog(Enums.Enums.LogLevel.ERROR, "Error occured", e.Exception);
            await Task.Delay(0);
        }


        private async Task _Client_MessageCreated(MessageCreateEventArgs e) {
            //For private users
            if (!e.Author.IsBot) {    //because this shit is retarded
            }
            //for channels
                switch (e.Channel.Name) {
                    default:
                        break;
                }
            
            await Task.Delay(0);
        }

        #endregion

    }
}


