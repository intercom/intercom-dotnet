using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using NUnit.Framework;

namespace Intercom.Test
{
    [TestFixture]
    public class UserConversationReplyTest : TestBase
    {
        public UserConversationReplyTest()
        {
        }

        [Test]
        public void UserConverstationReply()
        {
            var mock = BuildSuccessMockClient<UserConversationsClient, Conversation>("Conversation.json", new Authentication(AppId, AppKey));

            var convo = mock.Reply(new UserConversationReply("147", "We noticed you using our Product,  do you have any questions?", "536e564f316c83104c000020"));

            Assert.IsNotNull(convo);
            Assert.IsTrue(convo.conversation_message.body.Contains("We noticed you using our Product"));
        }

        [Test]
        public void ListUserConversations()
        {
            var mock = BuildSuccessMockClient<UserConversationsClient, Conversation>("ConversationsList.json", new Authentication(AppId, AppKey));

            var convo = mock.List("536e564f316c83104c000020");

            Assert.IsNotNull(convo);
            Assert.AreEqual(1, convo.conversations.Count);
        }
    }
}