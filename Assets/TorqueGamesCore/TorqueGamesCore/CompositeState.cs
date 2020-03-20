using System.Collections.Generic;

namespace Core
{
    public class CompositeState : IGameComponentState
    {
        private Dictionary<long,IGameComponentState> states = new Dictionary<long, IGameComponentState>();
        
        public IGameComponentState this[long key]
        {
            get => states.TryGetValue(key, out var value) ? value : null;
            set => states[key] = value;
        }
        
        public void Clear()=> states.Clear();

        public void Load(CompositeState state)
        {
            states = state.states;
        }
    }
}