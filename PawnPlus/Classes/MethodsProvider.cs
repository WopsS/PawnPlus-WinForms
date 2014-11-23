using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus
{
    public class MethodsProvider
    {
        private static Dictionary<string, MethodInformations> Methods = new Dictionary<string, MethodInformations>();

        public static Dictionary<string, MethodInformations> InitializeMethods()
        {
            #region Scripting Functions A

            Methods.Add("AddMenuItem", new MethodInformations
            {
                Description = "Adds an item to a specified menu.",
                Parameters = new string[] 
                {
                    "Menu:menuid", "column", "title[]" 
                },
                ParametersDescription = new string[] 
                { 
                    "The menu id to add an item to.", "The column to add the item to.", "The title for the new menu item"
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("AddPlayerClass", new MethodInformations
            {
                Description = "Adds a class to class selection.",
                Parameters = new string[] 
                {
                    "skin", "Float:x", "Float:y", "Float:z", "Float:Angle", "weapon1", "weapon1_ammo", "weapon2", "weapon2_ammo", "weapon3", "weapon3_ammo"
                },
                ParametersDescription = new string[] 
                { 
                    "The skin which the player will spawn with.", "The X coordinate of the spawnpoint of this class.", "The Y coordinate of the spawnpoint of this class.", 
                    "The Z coordinate of the spawnpoint of this class.", "The direction in which the player should face after spawning.", "The first spawn-weapon for the player.", 
                    "The amount of ammunition for the primary spawn weapon.", "The second spawn-weapon for the player.", "The amount of ammunition for the second spawn weapon.", 
                    "The third spawn-weapon for the player.", "The amount of ammunition for the third spawn weapon."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the class which was just added. "
                }
            });

            Methods.Add("AddPlayerClassEx", new MethodInformations
            {
                Description = "Adds a class to class selection with the addition of a team parameter.",
                Parameters = new string[] 
                {
                    "teamid", "skin", "Float:x", "Float:y", "Float:z", "Float:Angle", "weapon1", "weapon1_ammo", "weapon2", "weapon2_ammo", "weapon3", "weapon3_ammo"
                },
                ParametersDescription = new string[] 
                { 
                    "The team you want the player to spawn in.", "The skin which the player will spawn with.", "The X coordinate of the spawnpoint of this class.", 
                    "The Y coordinate of the spawnpoint of this class.", "The Z coordinate of the spawnpoint of this class.", "The direction in which the player should face after spawning.", 
                    "The first spawn-weapon for the player.", "The amount of ammunition for the primary spawn weapon.", "The second spawn-weapon for the player.", 
                    "The amount of ammunition for the second spawn weapon.", "The third spawn-weapon for the player.", "The amount of ammunition for the third spawn weapon."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the class which was just added. "
                }
            });

            Methods.Add("AddStaticPickup", new MethodInformations
            {
                Description = "This function adds a 'static' pickup to the game.",
                Parameters = new string[] 
                {
                    "model", "type", "Float:X", "Float:Y", "Float:Z", "Virtualworld"
                },
                ParametersDescription = new string[] 
                { 
                    "The model of the pickup.", "The pickup spawn type.", "The X coordinate to create the pickup at.",
                    "The Y coordinate to create the pickup at.", "The Z coordinate to create the pickup at.", "The virtual world ID of the pickup. Use -1 to show the pickup in all worlds."
                },
                ReturnValues = new int[] 
                { 
                    1,
                },
                ReturnValueDescription = new string[] 
                { 
                    "if the pickup is successfully created"
                }
            });

            Methods.Add("AddStaticVehicle", new MethodInformations
            {
                Description = "Adds a 'static' vehicle (models are pre-loaded for players) to the gamemode.",
                Parameters = new string[] 
                {
                    "modelid", "Float:spawn_x", "Float:spawn_y", "Float:spawn_z", 
                    "Float:angle", "color1", "color2"
                },
                ParametersDescription = new string[] 
                { 
                    "The Model ID for the vehicle.", "The X-coordinate for the vehicle.", "The Y-coordinate for the vehicle.", 
                    "The Z-coordinate for the vehicle.", "Direction of vehicle - angle.", "The primary color ID. -1 for random.", 
                    "The secondary color ID. -1 for random."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The vehicle ID of the vehicle created (between 1 and MAX_VEHICLES).",
                    "INVALID_VEHICLE_ID (65535) if vehicle was not created (vehicle limit reached or invalid vehicle model ID passed)."
                }
            });

            Methods.Add("AddStaticVehicleEx", new MethodInformations
            {
                Description = "Adds a 'static' vehicle with a re-spawn delay (models are pre-loaded for players) to the gamemode.",
                Parameters = new string[] 
                {
                    "modelid", "Float:spawn_x", "Float:spawn_y", "Float:spawn_z", 
                    "Float:angle", "color1", "color2", "respawn_delay"
                },
                ParametersDescription = new string[] 
                { 
                    "The Model ID for the vehicle.", "The X-coordinate for the vehicle.", "The Y-coordinate for the vehicle.", 
                    "The Z-coordinate for the vehicle.", "Direction of vehicle - angle.", "The primary color ID. -1 for random.", 
                    "The secondary color ID. -1 for random.", "The delay until the car is respawned without a driver, in seconds."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The vehicle ID of the vehicle created (between 1 and MAX_VEHICLES).",
                    "INVALID_VEHICLE_ID (65535) if vehicle was not created (vehicle limit reached or invalid vehicle model ID passed)."
                }
            });

            Methods.Add("AddVehicleComponent", new MethodInformations
            {
                Description = "Adds a 'component' (often referred to as a 'mod' (modification)) to a vehicle",
                Parameters = new string[] 
                {
                    "vehicleid", "componentid"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the vehicle to add the component to. Not to be confused with modelid.", "The ID of the component to add to the vehicle."
                },
                ReturnValues = new int[] 
                { 
                    0,
                    1
                },
                ReturnValueDescription = new string[] 
                { 
                    "The component was not added because the vehicle does not exist. ",
                    "The component was successfully added to the vehicle."
                }
            });

            Methods.Add("AllowInteriorWeapons", new MethodInformations
            {
                Description = "Toggle whether the usage of weapons in interiors is allowed or not.",
                Parameters = new string[] 
                {
                    "allow"
                },
                ParametersDescription = new string[] 
                { 
                    "1 to enable weapons in interiors (enabled by default), 0 to disable weapons in interiors."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("ApplyAnimation", new MethodInformations
            {
                Description = "Apply an animation to a player.",
                Parameters = new string[] 
                {
                    "playerid", "animlib[]", "animname[]", "Float:fDelta", 
                    "loop", "lockx", "locky", "freeze", "time", 
                    "forcesync"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player to apply the animation to.", "The animation library from which to apply an animation.", "The name of the animation to apply, within the specified library.", 
                    "The speed to play the animation (use 4.1).", "If set to 1, the animation will loop. If set to 0, the animation will play once.", "	If set to 0, the player is returned to their old X coordinate once the animation is complete (for animations that move the player such as walking). 1 will not return them to their old position.", 
                    "Same as above but for the Y axis. Should be kept the same as the previous parameter.", "Setting this to 1 will freeze the player at the end of the animation. 0 will not.", "Timer in milliseconds. For a never-ending loop it should be 0.", 
                    "Set to 1 to make server sync the animation with all other players in streaming radius (optional). 2 works same as 1, but will ONLY apply the animation to streamed-in players, but NOT the actual player being animated (useful for npc animations and persistent animations when players are being streamed) "
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function always returns 1, even if the player specified does not exist, or any of the parameters are invalid (e.g. invalid library)."
                }
            });

            Methods.Add("Attach3DTextLabelToPlayer", new MethodInformations
            {
                Description = "Attach a 3D text label to a player.",
                Parameters = new string[] 
                {
                    "Text3D:id", "playerid", "Float:OffsetX", "Float:OffsetY", "Float:OffsetZ"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the 3D text label to attach.", "The ID of the player to attach the label to.", "The X offset from the player.",
                    "The Y offset from the player.", "The Z offset from the player."
                },
                ReturnValues = new int[] 
                { 
                    0,
                    1
                },
                ReturnValueDescription = new string[] 
                { 
                    "The function failed to execute. This means the player and/or label do not exist.",
                    "The function executed successfully."
                }
            });

            Methods.Add("Attach3DTextLabelToVehicle", new MethodInformations
            {
                Description = "Attaches a 3D Text Label to a specific vehicle.",
                Parameters = new string[] 
                {
                    "Text3D:id", "vehicleid", "Float:OffsetX", 
                    "Float:OffsetY", "Float:OffsetZ"
                },
                ParametersDescription = new string[] 
                { 
                    "The 3D Text Label you want to attach.", "The vehicle you want to attach the 3D Text Label to.", "The Offset-X coordinate of the player vehicle (the vehicle is 0.0,0.0,0.0).",
                    "The Offset-Y coordinate of the player vehicle (the vehicle is 0.0,0.0,0.0).", "The Offset-Z coordinate of the player vehicle (the vehicle is 0.0,0.0,0.0)."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("AttachCameraToObject", new MethodInformations
            {
                Description = "You can use this function to attach the player camera to objects.",
                Parameters = new string[] 
                {
                    "playerid", "objectid" 
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player which will have your camera attached on object.", "The object id which you want to attach the player camera."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("AttachCameraToPlayerObject", new MethodInformations
            {
                Description = "Attaches a player's camera to a player-object.",
                Parameters = new string[] 
                {
                    "playerid", "playerobjectid" 
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player which will have their camera attached to a player-object.", "The ID of the player-object to which the player's camera will be attached."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("AttachObjectToObject", new MethodInformations
            {
                Description = "Attach objects to other objects. The objects will folow the main object.",
                Parameters = new string[] 
                {
                    "objectid", "attachtoid", "Float:OffsetX", 
                    "Float:OffsetY", "Float:OffsetZ", "Float:RotX",
                    "Float:RotY", "Float:RotZ", "SyncRotation = 1"
                },
                ParametersDescription = new string[] 
                { 
                    "The object to attach to another object.", "The object to attach the object to.", "The distance between the main object and the object in the X direction.",
                    "The distance between the main object and the object in the Y direction.", "The distance between the main object and the object in the Z direction.", "The X rotation between the object and the main object.",
                    "The Y rotation between the object and the main object.", "The Z rotation between the object and the main object.", "If set to 0, objectid's rotation will not change with attachtoid's."
                },
                ReturnValues = new int[] 
                { 
                    0,
                    1
                },
                ReturnValueDescription = new string[] 
                { 
                    "The function failed to execute. This means the first object (objectid) does not exist.",
                    "The function executed successfully."
                }
            });

            Methods.Add("AttachObjectToPlayer", new MethodInformations
            {
                Description = "Attach an object to a player.",
                Parameters = new string[] 
                {
                    "objectid", "playerid", "Float:OffsetX", 
                    "Float:OffsetY", "Float:OffsetZ", "Float:rX", 
                    "Float:rY", "Float:rZ" 
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the object to attach to the player.", "The ID of the player to attach the object to.", "The distance between the player and the object in the X direction.", 
                    "The distance between the player and the object in the Y direction.","The distance between the player and the object in the Z direction.", "The X rotation between the object and the player.", 
                    "The Y rotation between the object and the player.", "The Z rotation between the object and the player.",
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function always returns 0."
                }
            });

            Methods.Add("AttachObjectToVehicle", new MethodInformations
            {
                Description = "Attach an object to a vehicle.",
                Parameters = new string[] 
                {
                    "objectid", "vehicleid", "Float:OffsetX", 
                    "Float:OffsetY", "Float:OffsetZ", "Float:RotX", 
                    "Float:RotY", "Float:RotZ"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the object to attach to the vehicle. Note that this is an object ID, not a model ID. The object must be CreateObject created first.", "The ID of the vehicle to attach the object to.", "The X axis offset from the vehicle to attach the object to.", 
                    "The Y axis offset from the vehicle to attach the object to.", "The Z axis offset from the vehicle to attach the object to.", "The X rotation offset for the object.", 
                    "The Y rotation offset for the object.", "The Z rotation offset for the object."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("AttachPlayerObjectToVehicle", new MethodInformations
            {
                Description = "Attach a player object to a vehicle.",
                Parameters = new string[] 
                {
                    "playerid", "objectid", "vehicleid", 
                    "Float:fOffsetX", "Float:fOffsetY", "Float:fOffsetZ", 
                    "Float:fRotX", "Float:fRotY", "Float:RotZ"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player the object was created for.", "The ID of the object to attach to the vehicle.", "The ID of the vehicle to attach the object to.", 
                    "The X position offset for attachment.", "The Y position offset for attachment.", "The Z position offset for attachment.",
                    "The X rotation offset for attachment.", "The Y rotation offset for attachment.", "The Z rotation offset for attachment."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("AttachTrailerToVehicle", new MethodInformations
            {
                Description = "Attach a vehicle to another vehicle as a trailer.",
                Parameters = new string[] 
                {
                    "trailerid", "vehicleid" 
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the vehicle that will be pulled.", "The ID of the vehicle that will pull the trailer."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function always returns 1, even if neither of the vehicle IDs passed are valid."
                }
            });

            #endregion

            #region Scripting Functions B

            Methods.Add("Ban", new MethodInformations
            {
                Description = "Ban a player who is currently in the server.",
                Parameters = new string[] 
                {
                    "playerid" 
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player to ban."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("BanEx", new MethodInformations
            {
                Description = "Ban a player with a reason.",
                Parameters = new string[] 
                {
                    "playerid", "reason" 
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player to ban.", "The reason for the ban."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("BlockIpAddress", new MethodInformations
            {
                Description = "Blocks an IP address from further communication with the server for a set amount of time (with wildcards allowed)",
                Parameters = new string[] 
                {
                    "ip_address[]", "timems" 
                },
                ParametersDescription = new string[] 
                { 
                    "The IP to block.", "The time (in milliseconds) that the connection will be blocked for. 0 can be used for an indefinite block."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            #endregion

            #region Scripting Functions C

            Methods.Add("CallLocalFunction", new MethodInformations
            {
                Description = "Calls a public function from the script in which it is used.",
                Parameters = new string[] 
                {
                    "const function[]", "const format[]", "{Float,_}:... "
                },
                ParametersDescription = new string[] 
                { 
                    "Public function's name.", "Tag/format of each variable.", "'Indefinite' number of arguments of any tag."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The value that the only public function returned."
                }
            });

            Methods.Add("CallRemoteFunction", new MethodInformations
            {
                Description = "Text",
                Parameters = new string[] 
                {
                    "const function[]", "const format[]", "{Float,_}:... "
                },
                ParametersDescription = new string[] 
                { 
                    "Public function's name.", "Tag/format of each variable.", "'Indefinite' number of arguments of any tag."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The value that the last public function returned."
                }
            });

            Methods.Add("CancelEdit", new MethodInformations
            {
                Description = "Cancel object edition mode for a player.",
                Parameters = new string[] 
                {
                    "playerid" 
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player to cancel edition for"
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("CancelSelectTextDraw", new MethodInformations
            {
                Description = "Cancel textdraw selection with the mouse",
                Parameters = new string[] 
                {
                    "playerid"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player that should be the textdraw selection disabled"
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("ChangeVehicleColor", new MethodInformations
            {
                Description = "Change a vehicle's primary and secondary colors.",
                Parameters = new string[] 
                {
                    "vehicleid", "color1", "color2"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the vehicle to change the colors of.", "The new vehicle's primary Color ID.", "The new vehicle's secondary Color ID."
                },
                ReturnValues = new int[] 
                { 
                    0,
                    1
                },
                ReturnValueDescription = new string[] 
                { 
                    "The function failed to execute. The vehicle does not exist.",
                    "The function executed successfully. The vehicle's color was successfully changed."
                }
            });

            Methods.Add("ChangeVehiclePaintjob", new MethodInformations
            {
                Description = "Change a vehicle's paintjob.",
                Parameters = new string[] 
                {
                    "vehicleid", "paintjobid"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the vehicle to change the paintjob of.", "The ID of the Paintjob to apply. Use 3 to remove a paintjob."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function always returns 1 (success), even if the vehicle passed is not created."
                }
            });

            Methods.Add("clamp", new MethodInformations
            {
                Description = "Force a value to be inside a range.",
                Parameters = new string[] 
                {
                    "value", "min=cellmin", "max=cellmax"
                },
                ParametersDescription = new string[] 
                { 
                    "The value to force in a range.", "The low bound of the range.", "The high bound of the range."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "value, if it is in the range min–max, min, if value is lower than min or max, if value is higher than max."
                }
            });

            Methods.Add("ClearAnimations", new MethodInformations
            {
                Description = "Clears all animations for the given player (it also cancels all current tasks such as jetpacking,parachuting,entering vehicles, driving (removes player out of vehicle), swimming, etc.).",
                Parameters = new string[] 
                {
                    "playerid", "forcesync=1"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player to clear the animations of.", "Set to 1 to force playerid to sync the animation with other players in streaming radius (optional)."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function always returns 1, even when the player specified is not connected."
                }
            });

            Methods.Add("ConnectNPC", new MethodInformations
            {
                Description = "Connect an NPC to the server.",
                Parameters = new string[] 
                {
                    "name[]", "script[]"
                },
                ParametersDescription = new string[] 
                { 
                    "The name the NPC should connect as. Must follow the same rules as normal player names.", "The NPC script name that is located in the npcmodes folder (without the .amx extension)."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            Methods.Add("Create3DTextLabel", new MethodInformations
            {
                Description = "Creates a 3D Text Label at a specific location in the world",
                Parameters = new string[] 
                {
                    "text[]", "color", "Float:X", 
                    "Float:Y", "Float:Z", "Float:DrawDistance", 
                    "virtualworld", "testLOS"
                },
                ParametersDescription = new string[] 
                { 
                    "The initial text string.", "The text Color.", "X-Coordinate.",
                    "Y-Coordinate.", "Z-Coordinate.", "The distance from where you are able to see the 3D Text Label.",
                    "The virtual world in which you are able to see the 3D Text.", "0/1 Test the line-of-sight so this text can't be seen through objects."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the newly created 3D Text Label"
                }
            });

            Methods.Add("CreateExplosion", new MethodInformations
            {
                Description = "Create an explosion at the specified coordinates.",
                Parameters = new string[] 
                {
                    "Float:X", "Float:Y", "Float:Z", 
                    "type", "Float:radius"
                },
                ParametersDescription = new string[] 
                { 
                    "The X coordinate of the explosion.", "Param1DescriptionThe Y coordinate of the explosion.", "The Z coordinate of the explosion.",
                    "The type of explosion.", "The explosion radius."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function always returns 1, even when the explosion type and/or radius values are invalid."
                }
            });

            Methods.Add("CreateExplosionForPlayer", new MethodInformations
            {
                Description = "Creates an explosion that is only visible to a single player.",
                Parameters = new string[] 
                {
                    "playerid", "Float:X", "Float:Y", 
                    "Float:Z", "type", "Float:Radius"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player to create the explosion for.", "The X coordinate of the explosion.", "The Y coordinate of the explosion.",
                    "The Z coordinate of the explosion.", "The explosion type.", "The radius of the explosion."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function always returns 1, even if the function failed to excute (player doesn't exist, invalid radius, or invalid explosion type)."
                }
            });

            Methods.Add("CreateMenu", new MethodInformations
            {
                Description = "Create a menu.",
                Parameters = new string[] 
                {
                    "title[]", "columns", "Float:x", 
                    "Float:y", "Float:col1width", "Float:col2width"
                },
                ParametersDescription = new string[] 
                { 
                    "The title for the new menu.", "How many colums shall the new menu have.", "The X position of the menu (640x460 canvas - 0 would put the menu at the far left).",
                    "The Y position of the menu (640x460 canvas - 0 would put the menu at the far top).", "The width for the first column.", "The width for the second column."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the new menu or -1 on failure."
                }
            });

            Methods.Add("CreateObject", new MethodInformations
            {
                Description = "Creates an object.",
                Parameters = new string[] 
                {
                    "modelid", "Float:X", "Float:Y", 
                    "Float:Z", "Float:rX", "Float:rY", 
                    "Float:rZ", "Float:DrawDistance"
                },
                ParametersDescription = new string[] 
                { 
                    "The model you want to use.", "The X coordinate to create the object at.", "The Y coordinate to create the object at.", 
                    "The Z coordinate to create the object at.", "The X rotation of the object.", "The Y rotation of the object.", 
                    "The Z rotation of the object.", "(optional) The distance that San Andreas renders objects at. 0.0 will cause objects to render at their default distances. Usable since 0.3b. Limited to 300 prior to 0.3x."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the object that was created."
                }
            });

            Methods.Add("CreatePickup", new MethodInformations
            {
                Description = "This function does exactly the same as AddStaticPickup, except it returns a pickup ID which can be used to destroy it afterwards and be tracked using OnPlayerPickUpPickup.",
                Parameters = new string[] 
                {
                    "model", "type", "Float:X", 
                    "Float:Y", "Float:Z", "Virtualworld"
                },
                ParametersDescription = new string[] 
                { 
                    "The model of the pickup.", "The pickup spawn type.", "The X coordinate to create the pickup at.",
                    "The Y coordinate to create the pickup at.", "The Z coordinate to create the pickup at.", "The virtual world ID of the pickup. Use -1 to make the pickup show in all worlds."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the created pickup, -1 on failure (pickup max limit)."
                }
            });

            Methods.Add("CreatePlayer3DTextLabel", new MethodInformations
            {
                Description = "Creates a 3D Text Label only for a specific player.",
                Parameters = new string[] 
                {
                    "playerid", "text[]", "color", 
                    "Float:X", "Float:Y", "Float:Z", 
                    "Float:DrawDistance", "attachedplayer", "attachedvehicle", 
                    "testLOS"
                },
                ParametersDescription = new string[] 
                { 
                    "The player which should see the newly created 3DText Label.", "The text to display.", "The text color.",
                    "X Coordinate (or offset if attached).", "Y Coordinate (or offset if attached).", "Z Coordinate (or offset if attached).",
                    "The distance where you are able to see the 3D Text Label.", "The player you want to attach the 3D Text Label to. (None: INVALID_PLAYER_ID)", "The vehicle you want to attach the 3D Text Label to. (None: INVALID_VEHICLE_ID)",
                    "0/1 Test the line-of-sight so this text can't be seen through walls"
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the newly created Player 3D Text Label"
                }
            });

            Methods.Add("CreatePlayerObject", new MethodInformations
            {
                Description = "Creates an object which will be visible to only one player.",
                Parameters = new string[] 
                {
                    "playerid", "modelid", "Float:X", 
                    "Float:Y", "Float:Z", "Float:rX", 
                    "Float:rY", "Float:rZ", "Float:DrawDistance = 0.0"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player to create the object for.", "The model to create.", "The X coordinate to create the object at.",
                    "The Y coordinate to create the object at.", "The Z coordinate to create the object at.", "The X rotation of the object.",
                    "The Y rotation of the object.", "The Z rotation of the object.", "The distance from which objects will appear to players. 0.0 will cause an object to render at its default distance. Leaving this parameter out will cause objects to be rendered at their default distance. The maximum usable distance is 300 in versions prior to 0.3x, in which drawdistance can be unlimited."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the object that was created, or INVALID_OBJECT_ID if the object limit (MAX_OBJECTS) was reached."
                }
            });

            Methods.Add("CreatePlayerTextDraw", new MethodInformations
            {
                Description = "Creates a textdraw for a single player.",
                Parameters = new string[] 
                {
                    "playerid", "Float:x", "Float:y", 
                    "text[]"
                },
                ParametersDescription = new string[] 
                { 
                    "The ID of the player to create the textdraw for.",  "X-Coordinate.",  "Y-Coordinate.",
                    "The text in the textdraw."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The ID of the created textdraw"
                }
            });

            Methods.Add("CreateVehicle", new MethodInformations
            {
                Description = "Creates a vehicle in the world.",
                Parameters = new string[] 
                {
                    "modelid", "Float:x", "Float:y", 
                    "Float:z", "Float:angle", "color1", 
                    "color2", "respawn_delay"
                },
                ParametersDescription = new string[] 
                { 
                    "The model for the vehicle.", "The X coordinate for the vehicle.", "The Y coordinate for the vehicle.",
                    "The Z coordinate for the vehicle.", "The facing angle for the vehicle.", "The primary color ID.",
                    "The secondary color ID.", "The delay until the car is respawned without a driver in seconds. Using -1 will prevent the vehicle from respawning."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "The vehicle ID of the vehicle created (between 1 and MAX_VEHICLES).",
                    "INVALID_VEHICLE_ID (65535) if vehicle was not created (vehicle limit reached or invalid vehicle model ID passed)."
                }
            });

            #endregion

            #region Scripting Functions D

            Methods.Add("db_close", new MethodInformations
            {
                Description = "Closes an SQLite database that was opened with db_open.",
                Parameters = new string[] 
                {
                    "ID"
                },
                ParametersDescription = new string[] 
                { 
                    "The 'handle' (ID) of the database to close (returned by db_open)."
                },
                ReturnValues = new int[] 
                { 
                    0,
                    1
                },
                ReturnValueDescription = new string[] 
                { 
                    "The function failed to execute. May mean that the database handle specified is not open.", 
                    "The function executed successfully. "
                }
            });

            Methods.Add("db_field_name", new MethodInformations
            {
                Description = "Returns the name of a field at a particular index.",
                Parameters = new string[] 
                {
                    "DBResult:dbresult", "field", "result[]", 
                    "maxlength"
                },
                ParametersDescription = new string[] 
                { 
                    "The result to get the data from; returned by db_query.", "The index of the field to get the name of.", "The result.",
                    "The max length of the field."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "Returns the name of a particular field."
                }
            });

            Methods.Add("db_free_result", new MethodInformations
            {
                Description = "Free result memory from a db_query.",
                Parameters = new string[] 
                {
                    "DBResult:dbresult"
                },
                ParametersDescription = new string[] 
                { 
                    "The result to free."
                },
                ReturnValues = new int[] 
                { 
                    0,
                    1
                },
                ReturnValueDescription = new string[] 
                { 
                    "The function failed to execute. There is no result stored, so no memory to free.",
                    "The function executed successfully. The memory was successfully freed."
                }
            });

            Methods.Add("db_get_field", new MethodInformations
            {
                Description = "Get the content of a field from db_query",
                Parameters = new string[] 
                {
                    "DBResult:dbresult", "field", "result[]", 
                    "maxlength"
                },
                ParametersDescription = new string[] 
                { 
                    "The result to get the data from.", "The field to get the data from.", "The result.",
                    "The max length of the field."
                },
                ReturnValues = null,
                ReturnValueDescription = new string[] 
                { 
                    "This function does not return any specific values."
                }
            });

            #endregion

            Methods.Add("Method", new MethodInformations
            {
                Description = "Text",
                Parameters = new string[] 
                {
                    "Param1", 
                    "Param2" 
                },
                ParametersDescription = new string[] 
                { 
                    "Param1Description",
                    "Param2Description"
                },
                ReturnValues = new int[] 
                { 
                    0,
                },
                ReturnValueDescription = new string[] 
                { 
                    "Return0"
                }
            });


            //Methods.Add("Method", new MethodInformations
            //{
            //    Description = "Text",
            //    Parameters = new string[] 
            //    {
            //        "Param1", 
            //        "Param2" 
            //    },
            //    ParametersDescription = new string[] 
            //    { 
            //        "Param1Description",
            //        "Param2Description"
            //    },
            //    ReturnValues = new int[] 
            //    { 
            //        0,
            //    },
            //    ReturnValueDescription = new string[] 
            //    { 
            //        "Return0"
            //    }
            //});

            return Methods;
        }
    }
}
