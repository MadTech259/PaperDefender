using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public abstract class GameComponentComposite : IGameComponent
    {
        public long UID { get; } = Guids.Create();

        protected CompositeState States { get; }

        protected ICollection<IGameComponent> Components { get; }
        
        private Task[] arrayTasks = new Task[1];

        protected GameComponentComposite()
        {
            States = new CompositeState();
            Components = new HashSet<IGameComponent>();
        }

        public virtual Task StartGame(IServicesInjector services)
        {
            if(Components.Count != arrayTasks.Length)
                arrayTasks = new Task[Components.Count];
            var i = 0;
            foreach (var gameComponent in Components)
            {
                arrayTasks[i] = gameComponent.StartGame(services);
                i++;
            }
            return Task.WhenAll(arrayTasks);
        }

        public virtual bool UtilLife => Components.All(c=>c.UtilLife);
        public virtual bool Active => Components.All(c=>c.Active);

        public virtual void Clear()
        {
            foreach (var gameComponent in Components)
            {
                gameComponent.Clear();
            }
            Components.Clear();
            States.Clear();
        }

        public virtual IGameComponentState SaveState()
        {
            foreach (var gameComponent in Components)
            {
                var uid = gameComponent.UID;
                var state = gameComponent.SaveState();
                States[uid] = state;
            }
            return States;
        }

        public virtual void LoadState(IGameComponentState state)
        {
            if (!(state is CompositeState)) return;

            States.Load((CompositeState) state);

            foreach (var gameComponent in Components)
            {
                var uid = gameComponent.UID;
                gameComponent.LoadState(States[uid]);
            }
        }
    }
}