using static System.Console;
using System.Collections.Generic;

namespace Mediator
{
    /// <summary>
    /// Mediator Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create chatroom participants
            var George = new Beatle { Name = "George" };
            var Paul = new Beatle { Name = "Paul" };
            var Ringo = new Beatle { Name = "Ringo" };
            var John = new Beatle { Name = "John" };
            var Yoko = new NonBeatle { Name = "Yoko" };

            // Create chatroom and register participants
            var chatroom = new Chatroom();
            chatroom.Register(George);
            chatroom.Register(Paul);
            chatroom.Register(Ringo);
            chatroom.Register(John);
            chatroom.Register(Yoko);

            // Chatting participants
            Yoko.Send("John", "Hi John!");
            Paul.Send("Ringo", "All you need is love");
            Ringo.Send("George", "My sweet Lord");
            Paul.Send("John", "Can't buy me love");
            John.Send("Yoko", "My sweet love");

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Mediator' interface
    /// </summary>
    public interface IChatroom
    {
        void Register(Participant participant);
        void Send(string from, string to, string message);
    }

    /// <summary>
    /// The 'ConcreteMediator' class
    /// </summary>
    public class Chatroom : IChatroom
    {
        private readonly Dictionary<string, Participant> participants = [];

        public void Register(Participant participant)
        {
            participants.TryAdd(participant.Name, participant);
            participant.Chatroom = this;
        }

        public void Send(string from, string to, string message)
        {
            var participant = participants[to];
            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }

    /// <summary>
    /// The 'AbstractColleague' class
    /// </summary>
    public class Participant
    {
        // Gets or sets participant name
        public string Name { get; set; } = null!;

        // Gets or sets chatroom
        public Chatroom Chatroom { get; set; } = null!;

        // Send a message to given participant
        public void Send(string to, string message)
        {
            Chatroom.Send(Name, to, message);
        }

        // Receive message from participant
        public virtual void Receive(string from, string message)
        {
            WriteLine($"{from} to {Name}: '{message}'");
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    public class Beatle : Participant
    {
        public override void Receive(string from, string message)
        {
            Write("To a Beatle: ");
            base.Receive(from, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    public class NonBeatle : Participant
    {
        public override void Receive(string from, string message)
        {
            Write("To a non-Beatle: ");
            base.Receive(from, message);
        }
    }
}