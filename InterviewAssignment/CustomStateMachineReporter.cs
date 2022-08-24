using Appccelerate.StateMachine.Machine;
using System;
using System.Collections.Generic;

namespace InterviewAssignment
{

    /// <summary>
    /// Provides way to customize state reporting.
    /// 
    /// By default you cannot get container of states from Appccelerate state machine.
    /// This class can be used for that. Also it has StateToString method to enable
    /// printing hierarchical states.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvent"></typeparam>
    public class CustomStateMachineReporter<TState, TEvent> : IStateMachineReport<TState, TEvent>
            where TState : IComparable
    where TEvent : IComparable

    {
        IEnumerable<IState<TState, TEvent>> myStates;

        public IEnumerable<IState<TState, TEvent>> States
        {
            get
            {
                return myStates;
            }
        }

        public void Report(string name, IEnumerable<IState<TState, TEvent>> states, Initializable<TState> initialStateId)
        {
            myStates = states;
        }

        public string StateToString(TState state, string separator = ".")
        {

            //Your assignment is here!
            //Tip: You find state machine hierarchy on States property (or myStates field). 
            //You should go through the states and print that on what hierarchy path the current state
            //is found. So if state is Initializing this method should return "Down.Initializing" because
            //"Initializing" is substate of "Down".
            string result = null;
            foreach (IState<TState, TEvent> s in States)
            {
                foreach(IState<TState, TEvent> subState in s.SubStates)
                {
                    if (subState.Id.Equals(state) && subState.SubStates.Count == 0)
                    {
                        result = s.ToString() + separator + subState.ToString();
                        break;
                    }
                    else if(subState.SubStates.Count != 0)
                    {
                        foreach (IState<TState, TEvent> subSubState in subState.SubStates)
                        {
                            if (subSubState.Id.Equals(state))
                            {
                                result = s.ToString() + separator + subState.ToString() + separator + subSubState.ToString();
                                break;
                            }
                        }
                    }
                }
                if (result != null) break;
            }
            return result;
        }
    }
}
