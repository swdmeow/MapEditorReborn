﻿namespace MapEditorReborn.API
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using UnityEngine;

    /// <summary>
    /// Component added to spawned LightControllerObject. Is is used for easier idendification of the object and it's variables.
    /// </summary>
    public class LightControllerComponent : MapEditorObject
    {
        /// <summary>
        /// The <see cref="Color"/> of affected rooms.
        /// </summary>
        public Color RoomColor = Color.red;

        /// <summary>
        /// The speed of color shifting.
        /// </summary>
        public float ShiftSpeed = 0f;

        /// <summary>
        /// Should room color be changed only, when the warhead has been started.
        /// </summary>
        public bool OnlyWarheadLight = false;

        /// <summary>
        /// The <see cref="RoomType"/> of affected rooms.
        /// </summary>
        public RoomType RoomType = RoomType.Unknown;

        /// <summary>
        /// Instantiates <see cref="LightControllerObject"/>.
        /// </summary>
        /// <param name="lightControllerObject"><see cref="LightControllerObject"/> used for instantiating the object. May be <see langword="null"/>.</param>
        public void Init(LightControllerObject lightControllerObject = null)
        {
            if (lightControllerObject != null)
            {
                RoomColor = new Color(lightControllerObject.Red, lightControllerObject.Green, lightControllerObject.Blue, lightControllerObject.Alpha);
                ShiftSpeed = lightControllerObject.ShiftSpeed;
                OnlyWarheadLight = lightControllerObject.OnlyWarheadLight;
                RoomType = lightControllerObject.RoomType;
            }
            else
            {
                RoomType = Map.FindParentRoom(gameObject).Type;
            }

            foreach (Room room in Map.Rooms.Where(x => x.Type == RoomType))
            {
                FlickerableLightController lightController = null;

                if (RoomType != RoomType.Surface)
                {
                    lightController = room.GetComponentInChildren<FlickerableLightController>();
                }
                else
                {
                    lightController = FindObjectsOfType<FlickerableLightController>().First(x => Map.FindParentRoom(x.gameObject).Type == RoomType.Surface);
                }

                if (lightController != null)
                {
                    lightControllers.Add(lightController);

                    lightController.Network_warheadLightColor = RoomColor;
                    lightController.Network_warheadLightOverride = !OnlyWarheadLight;
                }
            }
        }

        private void Start() => currentColor = RoomColor;

        private void Update()
        {
            if (ShiftSpeed == 0f)
                return;

            currentColor = ShiftHueBy(currentColor, ShiftSpeed * Time.deltaTime);

            foreach (FlickerableLightController lightController in lightControllers)
            {
                lightController.Network_warheadLightColor = currentColor;
            }
        }

        private void OnDestroy()
        {
            foreach (FlickerableLightController lightController in lightControllers)
            {
                lightController.Network_warheadLightColor = FlickerableLightController.DefaultWarheadColor;
                lightController.Network_warheadLightOverride = false;
            }
        }

        // Credits to Killers0992
        private Color ShiftHueBy(Color color, float amount)
        {
            // convert from RGB to HSV
            Color.RGBToHSV(color, out float hue, out float saturation, out float value);

            // shift hue by amount
            hue += amount;

            // convert back to RGB and return the color
            return Color.HSVToRGB(hue, saturation, value);
        }

        private Color currentColor;

        private List<FlickerableLightController> lightControllers = new List<FlickerableLightController>();
    }
}