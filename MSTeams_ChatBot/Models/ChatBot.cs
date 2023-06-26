using Microsoft.Bot.Builder;

namespace MSTeams_ChatBot.Models
{
    public class ChatBot
    {
        public ConversationState ConversationState { get; }
        public UserState UserState { get; }
        public ChatBot(ConversationState conversationState, UserState userState)
        {
            ConversationState = conversationState;
            UserState = userState;
        }

    }
}
