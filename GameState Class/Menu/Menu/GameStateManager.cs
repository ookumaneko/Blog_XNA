using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Sample
{
    enum GameStates
    {
        Menu,
        Playing,
        Paused,
        Shop,
        Transition,
    }

    class GameStateManager
    {
        Dictionary<GameStates, GameState> m_states;
        GameStates m_currentState;

        public GameStateManager()
        {
            m_states = new Dictionary<GameStates, GameState>();
            m_currentState = GameStates.Menu;
        }

        public GameStates CurrentState
        {
            get { return m_currentState; }
            set { m_currentState = value; }
        }

        public void AddState(GameState toAdd, GameStates type)
        {
            m_states.Add(type, toAdd);
        }

        public bool RemoveState(GameStates toRemove)
        {
            return m_states.Remove(toRemove);
        }

        public void ChangeState(GameStates newState)
        {
            m_currentState = newState;
        }

        public void Update(GameTime gameTime)
        {
            GameState state;

            //現在のstateがある場合、保存されているGameStateへの参照を得る
            if (m_states.TryGetValue(m_currentState, out state))
            {
                // 現在のシーンを更新
                state.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            GameState state;

            //if the current GameState exist, copy ref to state then call Drawmethod in that state.
            if (m_states.TryGetValue(m_currentState, out state))
            {
                state.Draw(gameTime);
            }
        }
    }
}
