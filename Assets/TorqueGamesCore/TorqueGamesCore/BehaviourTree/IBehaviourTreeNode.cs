using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Injector;
using TorqueGamesCore.Injector;

namespace TorqueGamesCore.BehaviourTree
{
    public enum BTState
    {
        Fail,
        Completed,
        Running,
    }

    public static class BtStateExtensions
    {
        public static BTState Invert(this BTState state)
        {
            if(state == BTState.Fail) return BTState.Completed;
            if(state == BTState.Completed) return BTState.Fail;
            return state;
        }
    }


    public abstract class BehaviourTreeNodeBase
    {
        protected IServicesInjector LocalDependencies { get; private set; }
        protected IServicesInjector GameDependencies  { get; private set; }
        
        public List<BehaviourTreeNodeBase> Children { get; } = new List<BehaviourTreeNodeBase>();
        public virtual void Init(IServicesInjector gamePlayServices){}
        public virtual void WriteLocalDependencies(IDependencyLinker linker){}
        public void ReadLocalDependencies(IServicesInjector localDependencies){}
        public abstract BTState Run();
        public virtual void Clean(){}
        public virtual void StartGame(){}
    }

    public interface IBehaviourTreeFactory
    {
        BehaviourTree Create(IServicesInjector gamePlayDependencies);
    }
    
    public class BehaviourTreeFactory<TBehaviourTree> : IBehaviourTreeFactory where TBehaviourTree : BehaviourTree, new() 
    {
        public BehaviourTree Create(IServicesInjector gamePlayDependencies)
        {
            var tree = new TBehaviourTree();
            tree.LoadTree();
            var nodes = tree.TreeNodes.ToList();
            
            var localInjector = TorqueGamesInjector.GetEmpty();
            var localLinker = localInjector.GetLinker();

            foreach (var node in nodes)
            {
                node.Init(gamePlayDependencies);
            }
            foreach (var node in nodes)
            {
                node.WriteLocalDependencies(localLinker);
            }
            foreach (var node in nodes)
            {
                node.ReadLocalDependencies(localInjector);
            }

            foreach (var node in nodes)
            {
                node.StartGame();
            }

            return tree;
        }
    }
    

    public abstract class BehaviourTree
    {
        private class Inverter : BehaviourTreeNodeBase
        {
            public override BTState Run() => Children[0].Run().Invert();
        }
        private class Sequencer : BehaviourTreeNodeBase
        {
            private int pc =0;
            public override BTState Run()
            {
                var result = Children[pc].Run();
                if (result == BTState.Fail)
                {
                    pc = 0;
                    return BTState.Fail;
                }
                pc = (pc + 1) % Children.Count;
                return result;
            }
        }

        private class Selector : BehaviourTreeNodeBase
        {
            private int pc =0;
            public override BTState Run()
            {
                var result = Children[pc].Run();
                if (result == BTState.Completed)
                {
                    pc = 0;
                    return BTState.Completed;
                }
                pc = (pc + 1) % Children.Count;
                return result;
            }
        }
        
        private class CompletedNode : BehaviourTreeNodeBase
        {
            public override BTState Run() => BTState.Completed;
        }
        
        private class FailNode : BehaviourTreeNodeBase
        {
            public override BTState Run() => BTState.Fail;
        }
        
        public static readonly BehaviourTreeNodeBase Fail = new FailNode(); 
        public static readonly BehaviourTreeNodeBase Completed = new CompletedNode(); 
        
        public BehaviourTreeNodeBase Root { get; set; }
        
        private HashSet<BehaviourTreeNodeBase> Nodes { get; } = new HashSet<BehaviourTreeNodeBase>();

        public IReadOnlyCollection<BehaviourTreeNodeBase> TreeNodes => Nodes;


        protected void AddChild(BehaviourTreeNodeBase parent, BehaviourTreeNodeBase child)
        {
            parent.Children.Add(child);
            Nodes.Add(child);
            Nodes.Add(parent);
        }

        protected BehaviourTreeNodeBase GetInverter()
        {
            return new Inverter();
        }
        protected BehaviourTreeNodeBase GetSelector()
        {
            return new Selector();
        }
        protected BehaviourTreeNodeBase GetSequencer()
        {
            return new Sequencer();
        }
        
        //todo: test node reacheability

        public abstract void LoadTree();

    }
}