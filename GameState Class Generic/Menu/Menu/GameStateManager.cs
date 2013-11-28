using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Sample
{
    class GameStateManager<T>
    {
        Dictionary<T, GameState<T>> m_states;
        T m_currentState;

        public GameStateManager()
        {
            m_states = new Dictionary<T, GameState<T>>();
        }

        public T CurrentState
        {
            get { return m_currentState; }
            set { m_currentState = value; }
        }

        public void AddState(GameState<T> toAdd, T type)
        {
            m_states.Add(type, toAdd);
            if (m_states.Count == 1)
            {
                m_currentState = type;
            }
        }

        public bool RemoveState(T toRemove)
        {
            return m_states.Remove(toRemove);
        }

        public void ChangeState(T newState)
        {
            m_currentState = newState;
        }

        public void Update(GameTime gameTime)
        {
            GameState<T> state;

            //現在のstateがある場合、保存されているGameStateへの参照を得る
            if (m_states.TryGetValue(m_currentState, out state))
            {
                // 現在のシーンを更新
                state.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            GameState<T> state;

            //if the current GameState exist, copy ref to state then call Drawmethod in that state.
            if (m_states.TryGetValue(m_currentState, out state))
            {
                state.Draw(gameTime);
            }
        }
    }
}
