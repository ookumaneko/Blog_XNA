using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SimpleParticle
{
    /// <summary>
    /// This class handles input for NADESICO library
    /// </summary>
    public static class Input
    {
        static GamePadState[] padState = new GamePadState[4];
        static GamePadState[] prevPadStates = new GamePadState[4];
        static KeyboardState keyState;
        static KeyboardState oldKeyState;

        static float[] timer = new float[4] { 0.0f, 0.0f, 0.0f, 0.0f };
        static bool[] isOn = new bool[4] { false, false, false, false };
        static float[] vibrationLength = new float[4] { 0.35f, 0.35f, 0.35f, 0.35f };


        //--------------------------------Member Methods---------------------------------------//

        /// <summary>
        /// Check whether a controller was disconnected this frame (i.e. it true is only returned once)
        /// </summary>
        /// <param name="index">Player Index of the controller to check</param>
        /// <returns>True if it was disconnected this frame, otherwise false</returns>
        public static bool WasDisconnected(PlayerIndex index)
        {
            return (!padState[(int)index].IsConnected && prevPadStates[(int)index].IsConnected);
        }

        /// <summary>
        /// Wait for a button press from a specific controller
        /// </summary>
        /// <param name="buttons">Buttons to check for the button presses</param>
        /// <param name="index">Index of the controller to check</param>
        /// <returns>True if one of the sent button was pressed. Otherwise returnns false</returns>
        public static bool WaitForButtonPress(Buttons[] buttons, ref PlayerIndex index)
        {
            for (int ii = 0; ii < 4; ++ii)
            {
                for (int jj = 0; jj < buttons.Length; ++jj)
                {
                    if (IsPressed((PlayerIndex)ii, buttons[jj]))
                    {
                        index = (PlayerIndex)ii;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a controller is connected and if it is a guiter controller or not
        /// </summary>
        /// <param name="index">Index of the controller</param>
        /// <returns>True if connected and NOT a guitar. Otherwise, returns false</returns>
        public static bool IsAcceptablePad(PlayerIndex index)
        {
            return (padState[(int)index].IsConnected &&
                    GamePad.GetCapabilities(index).GamePadType != GamePadType.Guitar &&
                    GamePad.GetCapabilities(index).GamePadType != GamePadType.AlternateGuitar);
        } 

        /// <summary>
        /// Set vibration for a controller.
        /// </summary>
        /// <param name="index">The controller to set vibration on</param>
        /// <param name="left">The strength of left motor vibration with value between 0.0f to 1.0f. 
        /// 0.0 being the weekest and 1.0f being the strongest </param>
        /// <param name="right">The strength of right motor vibration with value between 0.0f to 1.0f. 
        /// 0.0 being the weekest and 1.0f being the strongest </param>
        /// <param name="duration">The length of the vibration</param>
        public static void SetVibration(PlayerIndex index, float left, float right, float duration)
        {
            GamePad.SetVibration(index, left, right);
            isOn[(int)index] = true;
            timer[(int)index] = 0;
            vibrationLength[(int)index] = duration;
        }

        /// <summary>
        /// Check if the given button was pressed this frame.
        /// If it was, returns true. Otherwise, returns false
        /// </summary>
        /// <param name="index">Which Controller to check.</param>
        /// <param name="button">Which button to check.</param>
        /// <returns>True if the button was just pressed; false otherwise</returns>
        public static bool IsPressed(PlayerIndex index, Buttons button)
        {
                return (padState[(int)index].IsButtonDown(button) && prevPadStates[(int)index].IsButtonUp(button) &&
                        IsAcceptablePad(index));
        }

        /// <summary>
        /// Check ig the givin key was pressed in this frame
        /// If it was, returns true. Otherwise, returns false.
        /// </summary>
        /// <param name="key">The keyboard key to check </param>
        /// <returns>True if key was just pressed, false otherwise</returns>
        public static bool IsPressed(Keys key)
        {
            return (keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key));
        }

        /// <summary>
        /// Check if the given button is down.
        /// If it is, returns true. Otherwise, returns false
        /// </summary>
        /// <param name="index">Which Controller to check.</param>
        /// <param name="button">Which button to check.</param>
        /// <returns>True if the button is down; false otherwise</returns>
        public static bool IsDown(PlayerIndex index, Buttons button)
        {
            {
                return ( padState[(int)index].IsButtonDown(button) && IsAcceptablePad(index) );
            }
        }

        /// <summary>
        /// Check ig the givin key is down.
        /// If it is, returns true. Otherwise, returns false.
        /// </summary>
        /// <param name="key">The keyboard key to check </param>
        /// <returns>True if key is down, false otherwise</returns>
        public static bool IsDown(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if the given button was released this frame.
        /// If it was, returns true. Otherwise, returns false
        /// </summary>
        /// <param name="index">Which Controller to check.</param>
        /// <param name="button">Which button to check.</param>
        /// <returns>True if the button was just released; false otherwise</returns>
        public static bool IsReleased(PlayerIndex index, Buttons button)
        {
            {
                return (padState[(int)index].IsButtonUp(button) && prevPadStates[(int)index].IsButtonDown(button) &&
                        IsAcceptablePad(index));
            }
        }

        /// <summary>
        /// Check ig the givin key was released in this frame
        /// If it was, returns true. Otherwise, returns false.
        /// </summary>
        /// <param name="key">The keyboard key to check </param>
        /// <returns>True if key was just released, false otherwise</returns>
        public static bool IsReleased(Keys key)
        {
            return (keyState.IsKeyUp(key) && oldKeyState.IsKeyDown(key));
        }

        #region Mouse Handling
        #if !XBOX360

        private static MouseState m_MouseState;
        private static MouseState m_OldMouseState;

        /// <summary>
        /// Check if left button of the mouse was pressed in the previous frame or not.
        /// If yes, return false, otherwise return true
        /// </summary>
        /// <returns></returns>
        public static bool LeftMouseButtonIsPressed()
        {
            return (m_MouseState.LeftButton == ButtonState.Pressed && m_OldMouseState.LeftButton == ButtonState.Released);
        }

        /// <summary>
        /// Check if right button of the mouse was pressed in the previous frame or not.
        /// If yes, return false, otherwise return true
        /// </summary>
        /// <returns></returns>
        public static bool RightMouseButtonIsPressed()
        {
            return (m_MouseState.RightButton == ButtonState.Pressed && m_OldMouseState.RightButton == ButtonState.Released);
        }

        public static int GetMouseX()
        {
            return m_MouseState.X;
        }

        public static int GetMouseY()
        {
            return m_MouseState.Y;
        }
        #endif
        #endregion

        /// <summary>
        /// Update states of all input devices
        /// </summary>
        public static void Update(float delta)
        {
            // コントローラーの更新
            for (int i = 0; i < 4; i++)
            {
                // 前のフレームの状態を保存
                prevPadStates[i] = padState[i];

                // 現在の状態を保存
                padState[i] = GamePad.GetState((PlayerIndex)i);

                // もし現在振動している場合
                if (isOn[i])
                {
                    // タイマーの更新
                    timer[i] += delta;

                    // もし設定した時間以上経過していた場合
                    if (timer[i] >= vibrationLength[i])
                    {
                        // 設定をリセット
                        isOn[i] = false;
                        timer[i] = 0;
                        vibrationLength[i] = 0.35f;

                        // 振動を止める
                        GamePad.SetVibration((PlayerIndex)i, 0.0f, 0.0f);
                    }
                }
            }

            // 前のフレームの状態を保存
            oldKeyState = keyState;

            // 現在の状態を保存
            keyState = Keyboard.GetState();

#if !XBOX360
            m_OldMouseState = m_MouseState;
            m_MouseState = Mouse.GetState();
#endif

        }
    }
}
