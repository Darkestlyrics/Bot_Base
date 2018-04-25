using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Base.Classes {
    class InternalCommands {

        [Command("GetCommands")]
        [Description("List all available commands")]
        [Cooldown(10, 120, CooldownBucketType.User)]
        [RequirePermissions(DSharpPlus.Permissions.SendMessages)]
        public async Task GetCommands(CommandContext ctx) {
            IReadOnlyDictionary<string, Command> commands = AppState.CommandsNextModule.RegisteredCommands;
            StringBuilder stringBuilder = new StringBuilder("Available Commands");
            stringBuilder.AppendLine("`");
            foreach (var item in commands.Where(o => o.Value.IsHidden == false)) {
                stringBuilder.Append($"{item.Value.Name} - {item.Value.Aliases}");
                stringBuilder.Append($"{item.Value.Description}");
            }
            stringBuilder.AppendLine("`");
            await ctx.RespondAsync(stringBuilder.ToString());
        }
    }
}
