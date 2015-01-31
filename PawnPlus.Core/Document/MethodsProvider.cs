using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus.Core.Document
{
    public class MethodInformations
    {
        public string Description { get; set; }

        public string[] Parameters { get; set; }
    }

    public class MethodsProvider
    {
        public static Dictionary<string, MethodInformations> Methods = new Dictionary<string, MethodInformations>();

        /// <summary>
        /// Return all SA-MP functions.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, MethodInformations> InitializeMethods()
        {
            Methods.Add("AddMenuItem", new MethodInformations
            {
                Description = "Adds an item to a specified menu.",
                Parameters = new string[]
                            {
                                "Menu:menuid", "column", "title[]", 
                            }
            });

            Methods.Add("AddPlayerClass", new MethodInformations
            {
                Description = "Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.",
                Parameters = new string[]
                {
                    "skin", "Float:x", "Float:y", "Float:z", "Float:Angle", "weapon1", "weapon1_ammo", "weapon2", "weapon2_ammo", "weapon3", "weapon3_ammo", 
                }
            });

            Methods.Add("AddPlayerClassEx", new MethodInformations
            {
                Description = "This function is exactly the same as the AddPlayerClass function, with the addition of a team parameter.",
                Parameters = new string[]
                {
                    "teamid", "skin", "Float:x", "Float:y", "Float:z", "Float:Angle", "weapon1", "weapon1_ammo", "weapon2", "weapon2_ammo", "weapon3", "weapon3_ammo", 
                }
            });

            Methods.Add("AddStaticPickup", new MethodInformations
            {
                Description = "This function adds a 'static' pickup to the game. These pickups support weapons, health, armor etc., with the ability to function without scripting them (weapons/health/armor will be given automatically).",
                Parameters = new string[]
                {
                    "model", "type", "Float:X", "Float:Y", "Float:Z", "Virtualworld", 
                }
            });

            Methods.Add("AddStaticVehicle", new MethodInformations
            {
                Description = "Adds a 'static' vehicle (models are pre-loaded for players) to the gamemode. Can only be used when the server first starts (under OnGameModeInit).",
                Parameters = new string[]
                {
                    "modelid", "Float:spawn_x", "Float:spawn_y", "Float:spawn_z", "Float:angle", "color1", "color2", 
                }
            });

            Methods.Add("AddStaticVehicleEx", new MethodInformations
            {
                Description = "Adds a 'static' vehicle (models are pre-loaded for players)to the gamemode. Can only be used when the server first starts (under OnGameModeInit). Differs from AddStaticVehicle in only one way: allows a respawn time to be set for when the vehicle is left unoccupied by the driver.",
                Parameters = new string[]
                {
                    "modelid", "Float:spawn_x", "Float:spawn_y", "Float:spawn_z", "Float:angle", "color1", "color2", "respawn_delay", 
                }
            });

            Methods.Add("AddVehicleComponent", new MethodInformations
            {
                Description = "Adds a 'component' (often referred to as a 'mod' (modification)) to a vehicle. Valid components can be found here.",
                Parameters = new string[]
                {
                    "vehicleid", "componentid", 
                }
            });

            Methods.Add("AllowAdminTeleport", new MethodInformations
            {
                Description = "This function will determine whether RCON admins will be teleported to their waypoint when they set one.",
                Parameters = new string[]
                {
                    "allow", 
                }
            });

            Methods.Add("AllowInteriorWeapons", new MethodInformations
            {
                Description = "Toggle whether the usage of weapons in interiors is allowed or not.",
                Parameters = new string[]
                {
                    "allow", 
                }
            });

            Methods.Add("AllowPlayerTeleport", new MethodInformations
            {
                Description = "Enable/Disable the teleporting ability for a player by right-clicking on the map.",
                Parameters = new string[]
                {
                    "playerid", "allow", 
                }
            });

            Methods.Add("ApplyAnimation", new MethodInformations
            {
                Description = "Apply an animation to a player.",
                Parameters = new string[]
                {
                    "playerid", "animlib[]", "animname[]", "Float:fDelta", "loop", "lockx", "locky", "freeze", "time", "forcesync", 
                }
            });

            Methods.Add("Attach3DTextLabelToPlayer", new MethodInformations
            {
                Description = "Attach a 3D text label to a player.",
                Parameters = new string[]
                {
                    "Text3D:id", "playerid", "Float:OffsetX", "Float:OffsetY", "Float:OffsetZ", 
                }
            });

            Methods.Add("Attach3DTextLabelToVehicle", new MethodInformations
            {
                Description = "Attaches a 3D Text Label to a specific vehicle.",
                Parameters = new string[]
                {
                    "Text3D:id", "vehicleid", "Float:OffsetX", "Float:OffsetY", "Float:OffsetZ", 
                }
            });

            Methods.Add("AttachCameraToObject", new MethodInformations
            {
                Description = "You can use this function to attach the player camera to objects.",
                Parameters = new string[]
                {
                    "playerid", "objectid", 
                }
            });

            Methods.Add("AttachCameraToPlayerObject", new MethodInformations
            {
                Description = "Attaches a player's camera to a player-object. The player is able to move their camera while it is attached to an object. Can be used with MovePlayerObject and AttachPlayerObjectToVehicle.",
                Parameters = new string[]
                {
                    "playerid", "playerobjectid", 
                }
            });

            Methods.Add("AttachObjectToObject", new MethodInformations
            {
                Description = "You can use this function to attach objects to other objects. The objects will folow the main object.",
                Parameters = new string[]
                {
                    "objectid", "attachtoid", "Float:OffsetX", "Float:OffsetY", "Float:OffsetZ", "Float:RotX", "Float:RotY", "Float:RotZ", "SyncRotation &#61; 1", 
                }
            });

            Methods.Add("AttachObjectToPlayer", new MethodInformations
            {
                Description = "Attach an object to a player.",
                Parameters = new string[]
                {
                    "objectid", "playerid", "Float:OffsetX", "Float:OffsetY", "Float:OffsetZ", "Float:rX", "Float:rY", "Float:rZ", 
                }
            });

            Methods.Add("AttachObjectToVehicle", new MethodInformations
            {
                Description = "Attach an object to a vehicle.",
                Parameters = new string[]
                {
                    "objectid", "vehicleid", "Float:OffsetX", "Float:OffsetY", "Float:OffsetZ", "Float:RotX", "Float:RotY", "Float:RotZ", 
                }
            });

            Methods.Add("AttachPlayerObjectToPlayer", new MethodInformations
            {
                Description = "The same as AttachObjectToPlayer but for objects which were created for player.",
                Parameters = new string[]
                {
                    "objectplayer", "objectid", "attachplayer", "Float:OffsetX", "Float:OffsetY", "Float:OffsetZ", "Float:rX", "Float:rY", "Float:rZ", 
                }
            });

            Methods.Add("AttachPlayerObjectToVehicle", new MethodInformations
            {
                Description = "Attach a player object to a vehicle.",
                Parameters = new string[]
                {
                    "playerid", "objectid", "vehicleid", "Float:fOffsetX", "Float:fOffsetY", "Float:fOffsetZ", "Float:fRotX", "Float:fRotY", "Float:RotZ", 
                }
            });

            Methods.Add("AttachTrailerToVehicle", new MethodInformations
            {
                Description = "Attach a vehicle to another vehicle as a trailer.",
                Parameters = new string[]
                {
                    "trailerid", "vehicleid", 
                }
            });

            Methods.Add("Ban", new MethodInformations
            {
                Description = "Ban a player who is currently in the server. They will be unable to join the server ever again. The ban will be IP-based, and be saved in the samp.ban file in the server's root directory. BanEx can be used to give a reason for the ban. IP bans can be added/removed using the RCON banip and unbanip commands (SendRconCommand.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("BanEx", new MethodInformations
            {
                Description = "Ban a player with a reason.",
                Parameters = new string[]
                {
                    "playerid", "reason[]", 
                }
            });

            Methods.Add("BlockIpAddress", new MethodInformations
            {
                Description = "Blocks an IP address from further communication with the server for a set amount of time (with wildcards allowed). Players trying to connect to the server with a blocked IP address will receive the generic \"You are banned from this server.\" message. Players that are online on the specified IP before the block will timeout after a few seconds and, upon reconnect, will receive the same message.",
                Parameters = new string[]
                {
                    "ip_address[]", "timems", 
                }
            });

            Methods.Add("CallLocalFunction", new MethodInformations
            {
                Description = "Calls a public function from the script in which it is used.",
                Parameters = new string[]
                {
                    "const function[]", "const format[]", "{Float", "_}:...", 
                }
            });

            Methods.Add("CallRemoteFunction", new MethodInformations
            {
                Description = "Calls a public function in any script that is loaded.",
                Parameters = new string[]
                {
                    "const function[]", "const format[]", "{Float", "_}:...", 
                }
            });

            Methods.Add("CancelEdit", new MethodInformations
            {
                Description = "Cancel object edition mode for a player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("CancelSelectTextDraw", new MethodInformations
            {
                Description = "Cancel textdraw selection with the mouse.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("ChangeVehicleColor", new MethodInformations
            {
                Description = "Change a vehicle's primary and secondary colors.",
                Parameters = new string[]
                {
                    "vehicleid", "color1", "color2", 
                }
            });

            Methods.Add("ChangeVehiclePaintjob", new MethodInformations
            {
                Description = "Change a vehicle's paintjob (for plain colors see ChangeVehicleColor).",
                Parameters = new string[]
                {
                    "vehicleid", "paintjobid", 
                }
            });

            Methods.Add("clamp", new MethodInformations
            {
                Description = "Force a value to be inside a range.",
                Parameters = new string[]
                {
                    "value", "min&#61;cellmin", "max&#61;cellmax", 
                }
            });

            Methods.Add("ClearAnimations", new MethodInformations
            {
                Description = "Clears all animations for the given player (it also cancels all current tasks such as jetpacking,parachuting,entering vehicles, driving (removes player out of vehicle), swimming, etc.. ).",
                Parameters = new string[]
                {
                    "playerid", "forcesync=1", 
                }
            });

            Methods.Add("ConnectNPC", new MethodInformations
            {
                Description = "Connect an NPC to the server.",
                Parameters = new string[]
                {
                    "name[]", "script[]", 
                }
            });

            Methods.Add("Create3DTextLabel", new MethodInformations
            {
                Description = "Creates a 3D Text Label at a specific location in the world.",
                Parameters = new string[]
                {
                    "text[]", "color", "Float:X", "Float:Y", "Float:Z", "Float:DrawDistance", "virtualworld", "testLOS", 
                }
            });

            Methods.Add("CreateExplosion", new MethodInformations
            {
                Description = "Create an explosion at the specified coordinates.",
                Parameters = new string[]
                {
                    "Float:X", "Float:Y", "Float:Z", "type", "Float:radius", 
                }
            });

            Methods.Add("CreateExplosionForPlayer", new MethodInformations
            {
                Description = "Creates an explosion that is only visible to a single player. This can be used to isolate explosions from other players or to make them only appear in specific virtual worlds.",
                Parameters = new string[]
                {
                    "playerid", "Float:X", "Float:Y", "Float:Z", "type", "Float:Radius", 
                }
            });

            Methods.Add("CreateMenu", new MethodInformations
            {
                Description = "Create a menu.",
                Parameters = new string[]
                {
                    "title[]", "columns", "Float:x", "Float:y", "Float:col1width", "Float:col2width", 
                }
            });

            Methods.Add("CreateObject", new MethodInformations
            {
                Description = "Creates an object.",
                Parameters = new string[]
                {
                    "modelid", "Float:X", "Float:Y", "Float:Z", "Float:rX", "Float:rY", "Float:rZ", "Float:DrawDistance", 
                }
            });

            Methods.Add("CreatePickup", new MethodInformations
            {
                Description = "This function does exactly the same as AddStaticPickup, except it returns a pickup ID which can be used to destroy it afterwards and be tracked using OnPlayerPickUpPickup.",
                Parameters = new string[]
                {
                    "model", "type", "Float:X", "Float:Y", "Float:Z", "Virtualworld", 
                }
            });

            Methods.Add("CreatePlayer3DTextLabel", new MethodInformations
            {
                Description = "Creates a 3D Text Label only for a specific player.",
                Parameters = new string[]
                {
                    "playerid", "text[]", "color", "Float:X", "Float:Y", "Float:Z", "Float:DrawDistance", "attachedplayer", "attachedvehicle", "testLOS", 
                }
            });

            Methods.Add("CreatePlayerObject", new MethodInformations
            {
                Description = "Creates an object which will be visible to only one player.",
                Parameters = new string[]
                {
                    "playerid", "modelid", "Float:X", "Float:Y", "Float:Z", "Float:rX", "Float:rY", "Float:rZ", "Float:DrawDistance = 0.0", 
                }
            });

            Methods.Add("CreatePlayerTextDraw", new MethodInformations
            {
                Description = "Creates a textdraw for a single player. This can be used as a way around the global text-draw limit.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "text[]", 
                }
            });

            Methods.Add("CreateVehicle", new MethodInformations
            {
                Description = "Creates a vehicle in the world. Can be used in place of AddStaticVehicleEx at any time in the script.",
                Parameters = new string[]
                {
                    "modelid", "Float:x", "Float:y", "Float:z", "Float:angle", "color1", "color2", "respawn_delay", 
                }
            });

            Methods.Add("db_close", new MethodInformations
            {
                Description = "Closes an SQLite database that was opened with db_open.",
                Parameters = new string[]
                {
                    "DB:db", 
                }
            });

            Methods.Add("db_field_name", new MethodInformations
            {
                Description = "Returns the name of a field at a particular index.",
                Parameters = new string[]
                {
                    "DBResult:dbresult", "field", "result[]", "maxlength", 
                }
            });

            Methods.Add("db_free_result", new MethodInformations
            {
                Description = "Frees result memory allocated from db_query.",
                Parameters = new string[]
                {
                    "DBResult:dbresult", 
                }
            });

            Methods.Add("db_get_field", new MethodInformations
            {
                Description = "Get the content of a field from db_query.",
                Parameters = new string[]
                {
                    "DBResult:dbresult", "field", "result[]", "maxlength", 
                }
            });

            Methods.Add("db_get_field_assoc", new MethodInformations
            {
                Description = "Get the contents of field with specified name.",
                Parameters = new string[]
                {
                    "DBResult:dbresult", "const field[]", "result[]", "maxlength", 
                }
            });

            Methods.Add("db_next_row", new MethodInformations
            {
                Description = "Moves to the next row of the result allocated from db_query.",
                Parameters = new string[]
                {
                    "DBResult:dbresult", 
                }
            });

            Methods.Add("db_num_fields", new MethodInformations
            {
                Description = "Get the number of fields in a result.",
                Parameters = new string[]
                {
                    "DBResult:dbresult", 
                }
            });

            Methods.Add("db_num_rows", new MethodInformations
            {
                Description = "Returns the number of rows from a db_query.",
                Parameters = new string[]
                {
                    "DBResult:dbresult", 
                }
            });

            Methods.Add("db_open", new MethodInformations
            {
                Description = "This function is used to open a connection to a SQLite database, which is inside the \"/scriptfiles\" folder.",
                Parameters = new string[]
                {
                    "name[]", 
                }
            });

            Methods.Add("db_query", new MethodInformations
            {
                Description = "This function is used to execute an SQL query on an opened SQLite database.",
                Parameters = new string[]
                {
                    "DB:db", "query[]", 
                }
            });

            Methods.Add("Delete3DTextLabel", new MethodInformations
            {
                Description = "Delete a 3D text label (created with Create3DTextLabel).",
                Parameters = new string[]
                {
                    "Text3D:id", 
                }
            });

            Methods.Add("DeletePVar", new MethodInformations
            {
                Description = "Deletes a previously set player variable.",
                Parameters = new string[]
                {
                    "playerid", "varname[]", 
                }
            });

            Methods.Add("DeletePlayer3DTextLabel", new MethodInformations
            {
                Description = "Destroy a 3D text label that was created using CreatePlayer3DTextLabel.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText3D:id", 
                }
            });

            Methods.Add("Deleteproperty", new MethodInformations
            {
                Description = "Delete an earlier set property (setproperty).",
                Parameters = new string[]
                {
                    "id=0", "const name[]=\"\"", "value=cellmin", 
                }
            });

            Methods.Add("DestroyMenu", new MethodInformations
            {
                Description = "Destroys the specified menu.",
                Parameters = new string[]
                {
                    "menuid", 
                }
            });

            Methods.Add("DestroyObject", new MethodInformations
            {
                Description = "Destroys (removes) the given object.",
                Parameters = new string[]
                {
                    "objectid", 
                }
            });

            Methods.Add("DestroyPickup", new MethodInformations
            {
                Description = "Destroys a pickup created with CreatePickup.",
                Parameters = new string[]
                {
                    "pickupid", 
                }
            });

            Methods.Add("DestroyPlayerObject", new MethodInformations
            {
                Description = "Destroy a player-object.",
                Parameters = new string[]
                {
                    "playerid", "objectid", 
                }
            });

            Methods.Add("DestroyVehicle", new MethodInformations
            {
                Description = "Destroy a vehicle. Instantly disappears.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("DetachTrailerFromVehicle", new MethodInformations
            {
                Description = "Detach the connection between a vehicle and its trailer, if any.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("DisableInteriorEnterExits", new MethodInformations
            {
                Description = "Disable all the interior entrances and exits in the game (the yellow arrows at doors).",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("DisableMenu", new MethodInformations
            {
                Description = "Disable a menu.",
                Parameters = new string[]
                {
                    "Menu:menuid", 
                }
            });

            Methods.Add("DisableMenuRow", new MethodInformations
            {
                Description = "Disable a specific row in a menu for all players. It will be greyed-out and can't be selected by players.",
                Parameters = new string[]
                {
                    "Menu:menuid", "row", 
                }
            });

            Methods.Add("DisableNameTagLOS", new MethodInformations
            {
                Description = "Disables the nametag Line-Of-Sight checking so that players can see nametags through objects.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("DisablePlayerCheckpoint", new MethodInformations
            {
                Description = "Disables (hides/destroys) a player's set checkpoint. Players can only have a single checkpoint set at a time. Checkpoints don't need to be disabled before setting another one.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("DisablePlayerRaceCheckpoint", new MethodInformations
            {
                Description = "Disable any initialized race checkpoints for a specific player, since you can only have one at any given time.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("DisableRemoteVehicleCollisions", new MethodInformations
            {
                Description = "Allows you to disable collisions between vehicles for a player.",
                Parameters = new string[]
                {
                    "playerid", "disable", 
                }
            });

            Methods.Add("EditAttachedObject", new MethodInformations
            {
                Description = "Enter edition mode for an attached object.",
                Parameters = new string[]
                {
                    "playerid", "index", 
                }
            });

            Methods.Add("EditObject", new MethodInformations
            {
                Description = "Allows a player to edit an object (position and rotation) using their mouse on a GUI (Graphical User Interface).",
                Parameters = new string[]
                {
                    "playerid", "objectid", 
                }
            });

            Methods.Add("EditPlayerObject", new MethodInformations
            {
                Description = "Allows players to edit a player-object (position and rotation) with a GUI and their mouse.",
                Parameters = new string[]
                {
                    "playerid", "objectid", 
                }
            });

            Methods.Add("EnableStuntBonusForAll", new MethodInformations
            {
                Description = "Enables or disables stunt bonuses for all players. If enabled, players will receive monetary rewards when performing a stunt in a vehicle (e.g. a wheelie).",
                Parameters = new string[]
                {
                    "enable", 
                }
            });

            Methods.Add("EnableStuntBonusForPlayer", new MethodInformations
            {
                Description = "Toggle stunt bonuses for a player. Enabled by default.",
                Parameters = new string[]
                {
                    "playerid", "enable", 
                }
            });

            Methods.Add("EnableTirePopping", new MethodInformations
            {
                Description = "With this function you can enable or disable tire popping.",
                Parameters = new string[]
                {
                    "show", 
                }
            });

            Methods.Add("EnableVehicleFriendlyFire", new MethodInformations
            {
                Description = "Enable friendly fire for team vehicles. Players will be unable to damage teammates' vehicles (SetPlayerTeam must be used!).",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("EnableZoneNames", new MethodInformations
            {
                Description = "This function allows to turn on zone / area names such as the \"Vinewood\" or \"Doherty\" text at the bottom-right of the screen as they enter the area. This is a gamemode option and should be set in the callback OnGameModeInit.",
                Parameters = new string[]
                {
                    "enable", 
                }
            });

            Methods.Add("existproperty", new MethodInformations
            {
                Description = "Check if a property exist.",
                Parameters = new string[]
                {
                    "id=0", "const name[]=\"\"", "value=cellmin", 
                }
            });

            Methods.Add("Fblockread", new MethodInformations
            {
                Description = "This function allows you to read data from a file, without encoding and line terminators.",
                Parameters = new string[]
                {
                    "{{{1}}}", 
                }
            });

            Methods.Add("fblockwrite", new MethodInformations
            {
                Description = "Write data to a file in binary format, while ignoring line brakes and encoding.",
                Parameters = new string[]
                {
                    "handle", "const buffer[]", "size = sizeof buffer", 
                }
            });

            Methods.Add("fclose", new MethodInformations
            {
                Description = "Closes a file. Files should always be closed when the script no longer needs them (after reading/writing).",
                Parameters = new string[]
                {
                    "File:handle", 
                }
            });

            Methods.Add("fexist", new MethodInformations
            {
                Description = "Checks if a specific file exists in the scriptfiles directory.",
                Parameters = new string[]
                {
                    "const pattern[]", 
                }
            });

            Methods.Add("fgetchar", new MethodInformations
            {
                Description = "Reads a single character from a file.",
                Parameters = new string[]
                {
                    "File: handle", "value", "bool: utf8=true", 
                }
            });

            Methods.Add("flength", new MethodInformations
            {
                Description = "Returns the length of a file.",
                Parameters = new string[]
                {
                    "File:handle", 
                }
            });

            Methods.Add("float", new MethodInformations
            {
                Description = "Converts an integer into a float.",
                Parameters = new string[]
                {
                    "value", 
                }
            });

            Methods.Add("floatabs", new MethodInformations
            {
                Description = "This function returns the absolute value of float.",
                Parameters = new string[]
                {
                    "Float:value", 
                }
            });

            Methods.Add("floatadd", new MethodInformations
            {
                Description = "Adds two floats together. This function is redundant as the standard operator (+) does the same thing. .",
                Parameters = new string[]
                {
                    "Float:Number1", "Float:Number2", 
                }
            });

            Methods.Add("floatcmp", new MethodInformations
            {
                Description = "floatcmp can be used to compare float values to each other, to validate the comparison. .",
                Parameters = new string[]
                {
                    "Float:oper1", "Float:oper2", 
                }
            });

            Methods.Add("floatcos", new MethodInformations
            {
                Description = "Get the cosine from a given angle. The input angle may be in radians, degrees or grades.",
                Parameters = new string[]
                {
                    "Float:value", "anglemode:mode=radian", 
                }
            });

            Methods.Add("floatdiv", new MethodInformations
            {
                Description = "Divide one float by another one. Redundant as the division operator (/) does the same thing.",
                Parameters = new string[]
                {
                    "Float:dividend", "Float:divisor", 
                }
            });

            Methods.Add("floatfract", new MethodInformations
            {
                Description = "Get the fractional part of a float. This means the value of the numbers after the decimal point.",
                Parameters = new string[]
                {
                    "Float:value", 
                }
            });

            Methods.Add("floatlog", new MethodInformations
            {
                Description = "This function allows you to get the logarithm of a float value.",
                Parameters = new string[]
                {
                    "Float:value", "Float:base", 
                }
            });

            Methods.Add("floatmul", new MethodInformations
            {
                Description = "Multiplies two floats with each other.",
                Parameters = new string[]
                {
                    "Float:oper1", "Float:oper2", 
                }
            });

            Methods.Add("floatpower", new MethodInformations
            {
                Description = "Raises the given value to the power of the exponent.",
                Parameters = new string[]
                {
                    "Float:value", "Float:exponent", 
                }
            });

            Methods.Add("floatround", new MethodInformations
            {
                Description = "Round a floating point number to an integer value.",
                Parameters = new string[]
                {
                    "Float:value", "method = floatround_round", 
                }
            });

            Methods.Add("floatsin", new MethodInformations
            {
                Description = "Get the sine from a given angle. The input angle may be in radians, degrees or grades.",
                Parameters = new string[]
                {
                    "Float:value", "anglemode:mode=radian", 
                }
            });

            Methods.Add("floatsqroot", new MethodInformations
            {
                Description = "Calculates the square root of given value.",
                Parameters = new string[]
                {
                    "Float:value", 
                }
            });

            Methods.Add("floatstr", new MethodInformations
            {
                Description = "Converts a string to a float.",
                Parameters = new string[]
                {
                    "const string[]", 
                }
            });

            Methods.Add("floatsub", new MethodInformations
            {
                Description = "Subtracts one float from another one. Note that this function has no real use, as one can simply use the standard operator (-) instead.",
                Parameters = new string[]
                {
                    "Float:oper1", "Float:oper2", 
                }
            });

            Methods.Add("floattan", new MethodInformations
            {
                Description = "Get the tangent from a given angle. The input angle may be in radians, degrees or grades.",
                Parameters = new string[]
                {
                    "Float:value", "anglemode:mode=radian", 
                }
            });

            Methods.Add("fmatch", new MethodInformations
            {
                Description = "Find a filename matching a pattern.",
                Parameters = new string[]
                {
                    "name[]", "const pattern[]", "index = 0", "size = sizeof name", 
                }
            });

            Methods.Add("fopen", new MethodInformations
            {
                Description = "Open a file (to read from or write to).",
                Parameters = new string[]
                {
                    "name[]", "filemode:mode = io_readwrite", 
                }
            });

            Methods.Add("ForceClassSelection", new MethodInformations
            {
                Description = "Forces a player to go back to class selection.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("format", new MethodInformations
            {
                Description = "Formats a string to include variables and other strings inside it.",
                Parameters = new string[]
                {
                    "output[]", "len", "const format[]", "{Float", "_}:...", 
                }
            });

            Methods.Add("fputchar", new MethodInformations
            {
                Description = "Write one character to a file.",
                Parameters = new string[]
                {
                    "File: handle", "value", "bool: utf8 = true", 
                }
            });

            Methods.Add("fread", new MethodInformations
            {
                Description = "Read a single line from a file.",
                Parameters = new string[]
                {
                    "File:handle", "string[]", "size = sizeof string", "bool: pack = false", 
                }
            });

            Methods.Add("fremove", new MethodInformations
            {
                Description = "Delete a file.",
                Parameters = new string[]
                {
                    "name[]", 
                }
            });

            Methods.Add("fseek", new MethodInformations
            {
                Description = "Change the current position in the file. You can either seek forward or backward through the file.",
                Parameters = new string[]
                {
                    "File:handle", "position", "whence", 
                }
            });

            Methods.Add("ftemp", new MethodInformations
            {
                Description = "Creates a file in the \"tmp\", \"temp\" or root directory with random name for reading and writing. The file is deleted after fclose() is used on the file.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("funcidx", new MethodInformations
            {
                Description = "This function returns the ID of a public function by its name.",
                Parameters = new string[]
                {
                    "const name[]", 
                }
            });

            Methods.Add("fwrite", new MethodInformations
            {
                Description = "Write text into a file.",
                Parameters = new string[]
                {
                    "File:handle", "string[]", 
                }
            });

            Methods.Add("GameModeExit", new MethodInformations
            {
                Description = "Ends the current gamemode.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("GameTextForAll", new MethodInformations
            {
                Description = "Shows 'game text' (on-screen text) for a certain length of time for all players.",
                Parameters = new string[]
                {
                    "const string[]", "time", "style", 
                }
            });

            Methods.Add("GameTextForPlayer", new MethodInformations
            {
                Description = "Shows 'game text' (on-screen text) for a certain length of time for a specific player.",
                Parameters = new string[]
                {
                    "playerid", "const string[]", "time", "style", 
                }
            });

            Methods.Add("GangZoneCreate", new MethodInformations
            {
                Description = "Create a gangzone (colored radar area).",
                Parameters = new string[]
                {
                    "Float:minx", "Float:miny", "Float:maxx", "Float:maxy", 
                }
            });

            Methods.Add("GangZoneDestroy", new MethodInformations
            {
                Description = "Destroy a gangzone.",
                Parameters = new string[]
                {
                    "zone", 
                }
            });

            Methods.Add("GangZoneFlashForAll", new MethodInformations
            {
                Description = "GangZoneFlashForAll flashes a gangzone for all players.",
                Parameters = new string[]
                {
                    "zone", "flashcolor", 
                }
            });

            Methods.Add("GangZoneFlashForPlayer", new MethodInformations
            {
                Description = "Makes a gangzone flash for a player.",
                Parameters = new string[]
                {
                    "playerid", "zone", "flashcolor", 
                }
            });

            Methods.Add("GangZoneHideForAll", new MethodInformations
            {
                Description = "GangZoneHideForAll hides a gangzone from all players.",
                Parameters = new string[]
                {
                    "zone", 
                }
            });

            Methods.Add("GangZoneHideForPlayer", new MethodInformations
            {
                Description = "Hides a gangzone for a player.",
                Parameters = new string[]
                {
                    "playerid", "zone", 
                }
            });

            Methods.Add("GangZoneShowForAll", new MethodInformations
            {
                Description = "Shows a gangzone with the desired color to all players.",
                Parameters = new string[]
                {
                    "zone", "color", 
                }
            });

            Methods.Add("GangZoneShowForPlayer", new MethodInformations
            {
                Description = "Show a gangzone for a player. Must be created with GangZoneCreate first.",
                Parameters = new string[]
                {
                    "playerid", "zone", "color", 
                }
            });

            Methods.Add("GangZoneStopFlashForAll", new MethodInformations
            {
                Description = "Stops a gangzone flashing for all players.",
                Parameters = new string[]
                {
                    "zone", 
                }
            });

            Methods.Add("GangZoneStopFlashForPlayer", new MethodInformations
            {
                Description = "Stops a gangzone flashing for a player.",
                Parameters = new string[]
                {
                    "playerid", "zone", 
                }
            });

            Methods.Add("GetAnimationName", new MethodInformations
            {
                Description = "Get the animation library/name for the index.",
                Parameters = new string[]
                {
                    "index", "animlib[]", "len1", "animname[]", "len2", 
                }
            });

            Methods.Add("GetGravity", new MethodInformations
            {
                Description = "Get the currently set gravity.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("GetMaxPlayers", new MethodInformations
            {
                Description = "Returns the maximum number of players that can join the server, as set by the server var 'maxplayers' in server.cfg.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("GetNetworkStats", new MethodInformations
            {
                Description = "Gets the server's network stats and stores them in a string.",
                Parameters = new string[]
                {
                    "retstr[]", "retstr_size", 
                }
            });

            Methods.Add("GetObjectModel", new MethodInformations
            {
                Description = "Allows you to retrieve the model ID of an object.",
                Parameters = new string[]
                {
                    "objectid", 
                }
            });

            Methods.Add("GetObjectPos", new MethodInformations
            {
                Description = "Get the position of an object.",
                Parameters = new string[]
                {
                    "objectid", "&amp;Float:X", "&amp;Float:Y", "&amp;Float:Z", 
                }
            });

            Methods.Add("GetObjectRot", new MethodInformations
            {
                Description = "Use this function to get the objects current rotation. The rotation is saved by reference in three RotX/RotY/RotZ variables.",
                Parameters = new string[]
                {
                    "objectid", "&amp;Float:RotX", "&amp;Float:RotY", "&amp;Float:RotZ", 
                }
            });

            Methods.Add("GetPVarFloat", new MethodInformations
            {
                Description = "Gets a player variable as a float.",
                Parameters = new string[]
                {
                    "playerid", "varname[]", 
                }
            });

            Methods.Add("GetPVarInt", new MethodInformations
            {
                Description = "Gets an integer player variable's value.",
                Parameters = new string[]
                {
                    "playerid", "varname[]", 
                }
            });

            Methods.Add("GetPVarNameAtIndex", new MethodInformations
            {
                Description = "Retrieve the name of a player's pVar via the index.",
                Parameters = new string[]
                {
                    "playerid", "index", "ret_varname[]", "ret_len", 
                }
            });

            Methods.Add("GetPVarString", new MethodInformations
            {
                Description = "Gets a player variable as a string.",
                Parameters = new string[]
                {
                    "playerid", "varname[]", "string_return[]", "len", 
                }
            });

            Methods.Add("GetPVarType", new MethodInformations
            {
                Description = "Gets the type (integer, float or string) of a player variable.",
                Parameters = new string[]
                {
                    "playerid", "varname[]", 
                }
            });

            Methods.Add("GetPVarsUpperIndex", new MethodInformations
            {
                Description = "Each PVar (player-variable) has its own unique identification number for lookup, this function returns the highest ID set for a player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerAmmo", new MethodInformations
            {
                Description = "Gets the amount of ammo in a player's current weapon.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerAnimationIndex", new MethodInformations
            {
                Description = "Returns the index of any running applied animations.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerArmour", new MethodInformations
            {
                Description = "This function stores the armour of a player into a variable.",
                Parameters = new string[]
                {
                    "playerid", "&amp;Float:armour", 
                }
            });

            Methods.Add("GetPlayerCameraAspectRatio", new MethodInformations
            {
                Description = "Retrieves the aspect ratio of a player's camera.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerCameraFrontVector", new MethodInformations
            {
                Description = "This function will return the current direction of player's aiming in 3-D space, the coords are relative to the camera position, see GetPlayerCameraPos.",
                Parameters = new string[]
                {
                    "playerid", "&amp;Float:x", "&amp;Float:y", "&amp;Float:z", 
                }
            });

            Methods.Add("GetPlayerCameraMode", new MethodInformations
            {
                Description = "Returns the current GTA camera mode for the requested player. The camera modes are useful in determining whether a player is aiming, doing a passenger driveby etc.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerCameraPos", new MethodInformations
            {
                Description = "Get the position of the player's camera.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("GetPlayerCameraTargetObject", new MethodInformations
            {
                Description = "Allows you to retrieve the Id of the object the player is looking at.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerCameraTargetPlayer", new MethodInformations
            {
                Description = "Allows you to retrieve the ID of the player the playerid is looking at.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerCameraTargetVehicle", new MethodInformations
            {
                Description = "Allows you to retrieve the ID of the vehicle the player is looking at.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerCameraUpVector", new MethodInformations
            {
                Description = "This function returns the vector, that points to the upside of the camera's view, or, in other words, to the middle top of your screen.",
                Parameters = new string[]
                {
                    "playerid", "&amp;Float:x", "&amp;Float:y", "&amp;Float:z", 
                }
            });

            Methods.Add("GetPlayerCameraZoom", new MethodInformations
            {
                Description = "Retrieves the game camera zoom level for a given player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerColor", new MethodInformations
            {
                Description = "Gets the color of the player's name and radar marker. Only works after SetPlayerColor.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerDistanceFromPoint", new MethodInformations
            {
                Description = "Calculate the distance between a player and a map coordinate.",
                Parameters = new string[]
                {
                    "playerid", "Float:X", "Float:Y", "Float:Z", 
                }
            });

            Methods.Add("GetPlayerDrunkLevel", new MethodInformations
            {
                Description = "Checks the player's level of drunkenness. If the level is less than 2000, the player is sober. The player's level of drunkness goes down slowly automatically (26 levels per second) but will always reach 2000 at the end (in 0.3b it will stop at zero). The higher drunkenness levels affect the player's camera, and the car driving handling. The level of drunkenness increases when the player drinks from a bottle (You can use SetPlayerSpecialAction to give them bottles).",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerFacingAngle", new MethodInformations
            {
                Description = "Gets the angle a player is facing.",
                Parameters = new string[]
                {
                    "playerid", "Float:Angle", 
                }
            });

            Methods.Add("GetPlayerFightingStyle", new MethodInformations
            {
                Description = "Get the fighting style the player currently using.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerHealth", new MethodInformations
            {
                Description = "The function GetPlayerHealth allows you to retrieve the health of a player. Useful for cheat detection, among other things.",
                Parameters = new string[]
                {
                    "playerid", "&amp;Float:health", 
                }
            });

            Methods.Add("GetPlayerInterior", new MethodInformations
            {
                Description = "Retrieves the player's current interior. A list of currently known interiors with their positions can be found on this page.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerIp", new MethodInformations
            {
                Description = "Get the specified player's IP address and store it in a string.",
                Parameters = new string[]
                {
                    "playerid", "name[]", "len", 
                }
            });

            Methods.Add("GetPlayerKeys", new MethodInformations
            {
                Description = "Check which keys a player is pressing.",
                Parameters = new string[]
                {
                    "playerid", "&amp;keys", "&amp;updown", "&amp;leftright", 
                }
            });

            Methods.Add("GetPlayerLastShotVectors", new MethodInformations
            {
                Description = "Retrieves the start and end (hit) position of the last bullet a player fired.",
                Parameters = new string[]
                {
                    "playerid", "&amp;Float:fOriginX", "&amp;Float:fOriginY", "&amp;Float:fOriginZ", "&amp;Float:fHitPosX", "&amp;Float:fHitPosY", "&amp;Float:fHitPosZ", 
                }
            });

            Methods.Add("GetPlayerMenu", new MethodInformations
            {
                Description = "Gets the ID of the menu the player is currently viewing (shown by ShowMenuForPlayer).",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerMoney", new MethodInformations
            {
                Description = "Retrieves the amount of money a player has.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerName", new MethodInformations
            {
                Description = "Get a player's name.",
                Parameters = new string[]
                {
                    "playerid", "const name[]", "len", 
                }
            });

            Methods.Add("GetPlayerNetworkStats", new MethodInformations
            {
                Description = "Gets a player's network stats and saves them into a string.",
                Parameters = new string[]
                {
                    "playerid", "retstr[]", "retstr_size", 
                }
            });

            Methods.Add("GetPlayerObjectModel", new MethodInformations
            {
                Description = "Allows you to retrieve the model ID of a player object.",
                Parameters = new string[]
                {
                    "objectid", 
                }
            });

            Methods.Add("GetPlayerObjectPos", new MethodInformations
            {
                Description = "Get the position of a player object (CreatePlayerObject).",
                Parameters = new string[]
                {
                    "playerid", "objectid", "&amp;Float:X", "&amp;Float:Y", "&amp;Float:Z", 
                }
            });

            Methods.Add("GetPlayerObjectRot", new MethodInformations
            {
                Description = "Use this function to get the object' s current rotation. The rotation is saved by reference in three RotX/RotY/RotZ variables.",
                Parameters = new string[]
                {
                    "playerid", "objectid", "&amp;Float:RotX", "&amp;Float:RotY", "&amp;Float:RotZ", 
                }
            });

            Methods.Add("GetPlayerPing", new MethodInformations
            {
                Description = "Get the ping of a player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerPos", new MethodInformations
            {
                Description = "Get the position of a player, represented by X, Y and Z coordinates.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("GetPlayerScore", new MethodInformations
            {
                Description = "This function returns a player's score as it was set using SetPlayerScore.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerSkin", new MethodInformations
            {
                Description = "Returns the class of the players skin.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerSpecialAction", new MethodInformations
            {
                Description = "Retrieves a player's current special action.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerState", new MethodInformations
            {
                Description = "Get a player's current state.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerSurfingObjectID", new MethodInformations
            {
                Description = "Returns the ID of the object the player is surfing on.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerSurfingVehicleID", new MethodInformations
            {
                Description = "Get the ID of the vehicle that the player is surfing (stuck to the roof of).",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerTargetPlayer", new MethodInformations
            {
                Description = "Check who a player is aiming at.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerTeam", new MethodInformations
            {
                Description = "Get the ID of the team the player is on.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerTime", new MethodInformations
            {
                Description = "Get the player's current game time. Set by SetWorldTime, SetWorldTime, or by the game automatically if TogglePlayerClock is used.",
                Parameters = new string[]
                {
                    "playerid", "&amp;hour", "&amp;minute", 
                }
            });

            Methods.Add("GetPlayerVehicleID", new MethodInformations
            {
                Description = "This function gets the ID of the vehicle the player is currently in. Note: NOT the model id of the vehicle. See GetVehicleModel for that.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerVehicleSeat", new MethodInformations
            {
                Description = "Find out which seat a player is in.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerVelocity", new MethodInformations
            {
                Description = "Get the velocity (speed) of a player on the X, Y and Z axes.",
                Parameters = new string[]
                {
                    "playerid", "&amp;Float:x", "&amp;Float:y", "&amp;Float:z", 
                }
            });

            Methods.Add("GetPlayerVersion", new MethodInformations
            {
                Description = "Returns the SA-MP client revision as reported by the player.",
                Parameters = new string[]
                {
                    "playerid", "version[]", "len", 
                }
            });

            Methods.Add("GetPlayerVirtualWorld", new MethodInformations
            {
                Description = "Retrieves the current virtual world the player is in.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerWantedLevel", new MethodInformations
            {
                Description = "Gets the wanted level of a player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerWeapon", new MethodInformations
            {
                Description = "Returns the ID of the weapon a player is currently holding.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetPlayerWeaponData", new MethodInformations
            {
                Description = "Get the weapon and ammo in a specific player's weapon slot (e.g. the weapon in the 'SMG' slot).",
                Parameters = new string[]
                {
                    "playerid", "slot", "&amp;weapons", "&amp;ammo", 
                }
            });

            Methods.Add("GetPlayerWeaponState", new MethodInformations
            {
                Description = "Check the state of a player's weapon.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("GetServerTickRate", new MethodInformations
            {
                Description = "Gets the tick rate (like FPS) of the server.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("GetServerVarAsBool", new MethodInformations
            {
                Description = "Gets a boolean server variable's value. Type 'varlist' in the console for a list of variables and their types.",
                Parameters = new string[]
                {
                    "const varname[]", 
                }
            });

            Methods.Add("GetServerVarAsInt", new MethodInformations
            {
                Description = "Get the integer value of a server variable, for example 'port'.",
                Parameters = new string[]
                {
                    "const varname[]", 
                }
            });

            Methods.Add("GetServerVarAsString", new MethodInformations
            {
                Description = "Retrieve a string server variable, for example 'hostname'. Typing 'varlist' in the console will display a list of available server variables.",
                Parameters = new string[]
                {
                    "const varname[]", "buffer[]", "len", 
                }
            });

            Methods.Add("GetTickCount", new MethodInformations
            {
                Description = "Returns the uptime of the actual server (not the SA-MP server) in milliseconds.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("GetVehicleComponentInSlot", new MethodInformations
            {
                Description = "Retrieves the installed component ID (modshop mod(ification)) on a vehicle in a specific slot.",
                Parameters = new string[]
                {
                    "vehicleid", "slot", 
                }
            });

            Methods.Add("GetVehicleComponentType", new MethodInformations
            {
                Description = "Find out what type of component a certain ID is.",
                Parameters = new string[]
                {
                    "component", 
                }
            });

            Methods.Add("GetVehicleDamageStatus", new MethodInformations
            {
                Description = "Retrieve the damage statuses of a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", "&amp;panels", "&amp;doors", "&amp;lights", "&amp;tires", 
                }
            });

            Methods.Add("GetVehicleDistanceFromPoint", new MethodInformations
            {
                Description = "This function can be used to calculate the distance (as a float) between a vehicle and another map coordinate. This can be useful to detect how far a vehicle away is from a location.",
                Parameters = new string[]
                {
                    "vehicleid", "Float:X", "Float:Y", "Float:Z", 
                }
            });

            Methods.Add("GetVehicleHealth", new MethodInformations
            {
                Description = "Get the health of a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", "&amp;Float:health", 
                }
            });

            Methods.Add("GetVehicleModel", new MethodInformations
            {
                Description = "Gets the model ID of a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("GetVehicleModelInfo", new MethodInformations
            {
                Description = "Retrieve information about a specific vehicle model such as the size or position of seats.",
                Parameters = new string[]
                {
                    "vehiclemodel", "infotype", "Float:X", "Float:Y", "Float:Z", 
                }
            });

            Methods.Add("GetVehiclePos", new MethodInformations
            {
                Description = "Gets the position of a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", "&amp;Float:X", "&amp;Float:Y", "&amp;Float:Z", 
                }
            });

            Methods.Add("GetVehicleRotationQuat", new MethodInformations
            {
                Description = "Returns a vehicle's rotation on all axes as a quaternion.",
                Parameters = new string[]
                {
                    "vehicleid", "&amp;Float:w", "&amp;Float:x", "&amp;Float:y", "&amp;Float:z", 
                }
            });

            Methods.Add("GetVehicleTrailer", new MethodInformations
            {
                Description = "Get the ID of the trailer attached to a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("GetVehicleVelocity", new MethodInformations
            {
                Description = "Get the velocity of a vehicle on the X, Y and Z axes.",
                Parameters = new string[]
                {
                    "vehicleid", "&amp;Float:x", "&amp;Float:y", "&amp;Float:z", 
                }
            });

            Methods.Add("GetVehicleVirtualWorld", new MethodInformations
            {
                Description = "Get the virtual world of a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("GetVehicleZAngle", new MethodInformations
            {
                Description = "Get the rotation of a vehicle on the Z axis (yaw).",
                Parameters = new string[]
                {
                    "vehicleid", "&amp;Float:z_angl", 
                }
            });

            Methods.Add("GetWeaponName", new MethodInformations
            {
                Description = "Get the name of a weapon.",
                Parameters = new string[]
                {
                    "weaponid", "const weapon[]", "len", 
                }
            });

            Methods.Add("getarg", new MethodInformations
            {
                Description = "Get an argument that was passed to a function.",
                Parameters = new string[]
                {
                    "arg", "index=0", 
                }
            });

            Methods.Add("getdate", new MethodInformations
            {
                Description = "Get the current server date, which will be stored in the variables &amp;year, &amp;month and &amp;day.",
                Parameters = new string[]
                {
                    "year", "month", "day", 
                }
            });

            Methods.Add("getproperty", new MethodInformations
            {
                Description = "Get a specific property from the memory, the string is returned as a packed string!.",
                Parameters = new string[]
                {
                    "id=0", "const name[]=\"\"", "value=cellmin", "string[]=\"\"", 
                }
            });

            Methods.Add("gettime", new MethodInformations
            {
                Description = "Get the current server time, which will be stored in the variables &amp;hour, &amp;minute and &amp;second.",
                Parameters = new string[]
                {
                    "&amp;hour&#61;0", "&amp;minute&#61;0", "&amp;second&#61;0", 
                }
            });

            Methods.Add("GivePlayerMoney", new MethodInformations
            {
                Description = "Give money to or take money from a player.",
                Parameters = new string[]
                {
                    "playerid", "money", 
                }
            });

            Methods.Add("GivePlayerWeapon", new MethodInformations
            {
                Description = "Give a player a weapon with a specified amount of ammo.",
                Parameters = new string[]
                {
                    "playerid", "weaponid", "ammo", 
                }
            });

            Methods.Add("HTTP", new MethodInformations
            {
                Description = "Sends a threaded HTTP request.",
                Parameters = new string[]
                {
                    "index", "type", "url[]", "data[]", "callback[]", 
                }
            });

            Methods.Add("heapspace", new MethodInformations
            {
                Description = "Returns the amount of memory available for the heap/stack in bytes.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("HideMenuForPlayer", new MethodInformations
            {
                Description = "Hides a menu for a player.",
                Parameters = new string[]
                {
                    "menuid", "playerid", 
                }
            });

            Methods.Add("InterpolateCameraLookAt", new MethodInformations
            {
                Description = "Interpolate a player's camera's 'look at' point between two coordinates with a set speed. Can be be used with InterpolateCameraPos.",
                Parameters = new string[]
                {
                    "playerid", "Float:FromX", "Float:FromY", "Float:FromZ", "Float:ToX", "Float:ToY", "Float:ToZ", "time", "cut = CAMERA_CUT", 
                }
            });

            Methods.Add("InterpolateCameraPos", new MethodInformations
            {
                Description = "Move a player's camera from one position to another, within the set time. Useful for scripted cut scenes.",
                Parameters = new string[]
                {
                    "playerid", "Float:FromX", "Float:FromY", "Float:FromZ", "Float:ToX", "Float:ToY", "Float:ToZ", "time", "cut = CAMERA_CUT", 
                }
            });

            Methods.Add("IsObjectMoving", new MethodInformations
            {
                Description = "Checks if the given objectid is moving.",
                Parameters = new string[]
                {
                    "objectid", 
                }
            });

            Methods.Add("IsPlayerAdmin", new MethodInformations
            {
                Description = "Check if a player is logged in as an RCON admin.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("IsPlayerAttachedObjectSlotUsed", new MethodInformations
            {
                Description = "Check if a player has an object attached in the specified index (slot).",
                Parameters = new string[]
                {
                    "playerid", "index", 
                }
            });

            Methods.Add("IsPlayerConnected", new MethodInformations
            {
                Description = "Checks if a player is connected (if an ID is taken by a connected player).",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("IsPlayerHoldingObject", new MethodInformations
            {
                Description = "Check if the player is holding an object.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("IsPlayerInAnyVehicle", new MethodInformations
            {
                Description = "Check if a player is inside any vehicle (as a driver or passenger).",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("IsPlayerInCheckpoint", new MethodInformations
            {
                Description = "Check if the player is currently inside a checkpoint, this could be used for properties or teleport points for example.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("IsPlayerInRaceCheckpoint", new MethodInformations
            {
                Description = "Check if the player is inside their current set race checkpoint (SetPlayerRaceCheckpoint).",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("IsPlayerInRangeOfPoint", new MethodInformations
            {
                Description = "Checks if a player is in range of a point. This native function is faster than the PAWN implementation using distance formula.",
                Parameters = new string[]
                {
                    "playerid", "Float:range", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("IsPlayerInVehicle", new MethodInformations
            {
                Description = "Checks if a player is in a specific vehicle.",
                Parameters = new string[]
                {
                    "playerid", "vehicleid", 
                }
            });

            Methods.Add("IsPlayerNPC", new MethodInformations
            {
                Description = "Check if a player is an actual player or an NPC.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("IsPlayerObjectMoving", new MethodInformations
            {
                Description = "Checks if the given player objectid is moving.",
                Parameters = new string[]
                {
                    "playerid", "objectid", 
                }
            });

            Methods.Add("IsPlayerStreamedIn", new MethodInformations
            {
                Description = "Checks if a player is streamed in another player's client.",
                Parameters = new string[]
                {
                    "playerid", "forplayerid", 
                }
            });

            Methods.Add("IsTrailerAttachedToVehicle", new MethodInformations
            {
                Description = "Checks if a vehicle has a trailer attached to it.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("IsValidObject", new MethodInformations
            {
                Description = "Checks if an object with the ID provided exists.",
                Parameters = new string[]
                {
                    "objectid", 
                }
            });

            Methods.Add("IsValidPlayerObject", new MethodInformations
            {
                Description = "Checks if the given object ID is valid for the given player.",
                Parameters = new string[]
                {
                    "playerid", "objectid", 
                }
            });

            Methods.Add("IsValidVehicle", new MethodInformations
            {
                Description = "Check if a vehicle is created.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("IsVehicleStreamedIn", new MethodInformations
            {
                Description = "Checks if a vehicle is streamed in for a player. Only nearby vehicles are streamed in (visible) for a player.",
                Parameters = new string[]
                {
                    "vehicleid", "forplayerid", 
                }
            });

            Methods.Add("Ispacked", new MethodInformations
            {
                Description = "Checks if the given string is packed.",
                Parameters = new string[]
                {
                    "const string[]", 
                }
            });

            Methods.Add("Kick", new MethodInformations
            {
                Description = "Kicks a player from the server. They will have to quit the game and re-connect if they wish to continue playing.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("KillTimer", new MethodInformations
            {
                Description = "Kills (stops) a running timer.",
                Parameters = new string[]
                {
                    "timerid", 
                }
            });

            Methods.Add("LimitGlobalChatRadius", new MethodInformations
            {
                Description = "Set a radius limitation for the chat. Only players at a certain distance from the player will see their message in the chat. Also changes the distance at which a player can see other players on the map at the same distance.",
                Parameters = new string[]
                {
                    "Float:chat_radius", 
                }
            });

            Methods.Add("LimitPlayerMarkerRadius", new MethodInformations
            {
                Description = "Set the player marker radius.",
                Parameters = new string[]
                {
                    "Float:marker_radius", 
                }
            });

            Methods.Add("LinkVehicleToInterior", new MethodInformations
            {
                Description = "Links a vehicle to an interior. Vehicles can only be seen by players in the same interior (SetPlayerInterior).",
                Parameters = new string[]
                {
                    "vehicleid", "interiorid", 
                }
            });

            Methods.Add("ManualVehicleEngineAndLights", new MethodInformations
            {
                Description = "Use this function before any player connects (OnGameModeInit) to tell all clients that the script will control vehicle engines and lights. This prevents the game automatically turning the engine on/off when players enter/exit vehicles and headlights automatically coming on when it is dark.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("memcpy", new MethodInformations
            {
                Description = "Copy bytes from one location to another.",
                Parameters = new string[]
                {
                    "dest[]", "const source[]", "index=0", "numbytes", "maxlength=sizeof dest", 
                }
            });

            Methods.Add("MoveObject", new MethodInformations
            {
                Description = "Move an object to a new position with a set speed. Players/vehicles will 'surf' the object as it moves.",
                Parameters = new string[]
                {
                    "objectid", "Float:X", "Float:Y", "Float:Z", "Float:Speed", "Float:RotX &#61; -1000.0", "Float:RotY &#61; -1000.0", "Float:RotZ &#61; -1000.0", 
                }
            });

            Methods.Add("MovePlayerObject", new MethodInformations
            {
                Description = "Move an object with a set speed. Also supports rotation. Players/vehicles will surf moving objects.",
                Parameters = new string[]
                {
                    "playerid", "objectid", "Float:X", "Float:Y", "Float:Z", "Float:Speed", "Float:RotX &#61; -1000.0", "Float:RotY &#61; -1000.0", "Float:RotZ &#61; -1000.0", 
                }
            });

            Methods.Add("NPC:IsPlayerStreamedIn", new MethodInformations
            {
                Description = "Checks if a player is streamed in for an NPC. Only nearby players are streamed in.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("NPC:IsVehicleStreamedIn", new MethodInformations
            {
                Description = "Checks if a vehicle is streamed in for an NPC. Only nearby vehicles are streamed in.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("NPC:OnNPCConnect", new MethodInformations
            {
                Description = "Gets called when a NPC successfully connects to the server.",
                Parameters = new string[]
                {
                    "myplayerid", 
                }
            });

            Methods.Add("NPC:OnNPCDisconnect", new MethodInformations
            {
                Description = "Gets called when the NPC gets disconnected from the server.",
                Parameters = new string[]
                {
                    "reason[]", 
                }
            });

            Methods.Add("NPC:OnNPCEnterVehicle", new MethodInformations
            {
                Description = "Gets called when a NPC enters a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", "seatid", 
                }
            });

            Methods.Add("NPC:OnNPCExitVehicle", new MethodInformations
            {
                Description = "Gets called when a NPC leaves a vehicle.",
                Parameters = new string[]
                {
                    "This callback has no parameters.", 
                }
            });

            Methods.Add("NPC:OnNPCModeExit", new MethodInformations
            {
                Description = "Gets called when a NPC-script unloaded.",
                Parameters = new string[]
                {
                    "NoParam", 
                }
            });

            Methods.Add("NPC:OnNPCModeInit", new MethodInformations
            {
                Description = "Gets called when a NPC script is loaded.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("NPC:OnNPCSpawn", new MethodInformations
            {
                Description = "Gets called when a NPC spawned.",
                Parameters = new string[]
                {
                    "NoParam", 
                }
            });

            Methods.Add("NPC:PauseRecordingPlayback", new MethodInformations
            {
                Description = "This will pause playing back the recording.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("NPC:ResumeRecordingPlayback", new MethodInformations
            {
                Description = "This will resume the paused recording.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("NPC:SendChat", new MethodInformations
            {
                Description = "This will send a player text by the bot, just like using SendPlayerMessageToAll, but this function is to be used inside the NPC scripts.",
                Parameters = new string[]
                {
                    "msg[]", 
                }
            });

            Methods.Add("NPC:SendCommand", new MethodInformations
            {
                Description = "This will force the NPC to write a desired command, and this way, getting the effects it would produce.",
                Parameters = new string[]
                {
                    "commandtext[]", 
                }
            });

            Methods.Add("NPC:StartRecordingPlayback", new MethodInformations
            {
                Description = "This will run a .rec file which has to be saved in the npcmodes/recordings folder. These files allow the NPC to follow certain actions. Their actions can be recorded manually. For more information, check the related functions.",
                Parameters = new string[]
                {
                    "playbacktype", "recordname[]", 
                }
            });

            Methods.Add("NPC:StopRecordingPlayback", new MethodInformations
            {
                Description = "This will stop the current .rec file which is being ran by the NPC, making it stay idle until some other order is given.",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("NetStats_BytesReceived", new MethodInformations
            {
                Description = "Gets the amount of data (in bytes) that the server has received from the player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("NetStats_BytesSent", new MethodInformations
            {
                Description = "Gets the amount of data (in bytes) that the server has sent to the player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("NetStats_ConnectionStatus", new MethodInformations
            {
                Description = "Gets the player's current connection status.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("NetStats_GetConnectedTime", new MethodInformations
            {
                Description = "Gets the amount of time (in milliseconds) that a player has been connected to the server for.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("NetStats_GetIpPort", new MethodInformations
            {
                Description = "Get a player's IP and port.",
                Parameters = new string[]
                {
                    "playerid", "ip_port[]", "ip_port_len", 
                }
            });

            Methods.Add("NetStats_MessagesReceived", new MethodInformations
            {
                Description = "Gets the number of messages the server has received from the player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("NetStats_MessagesRecvPerSecond", new MethodInformations
            {
                Description = "Gets the number of messages the player has sent in the last second.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("NetStats_MessagesSent", new MethodInformations
            {
                Description = "Gets the number of messages the server has sent to the player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("NetStats_PacketLossPercent", new MethodInformations
            {
                Description = "Gets the packet loss percentage of a player. Packet loss means data the player is sending to the server is being lost (or vice-versa).",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("numargs", new MethodInformations
            {
                Description = "Get the number of arguments passed to a function.",
                Parameters = new string[]
                {
                    "This function has no parameters", 
                }
            });

            Methods.Add("PlayAudioStreamForPlayer", new MethodInformations
            {
                Description = "Play an 'audio stream' for a player. Normal audio files also work (e.g. MP3).",
                Parameters = new string[]
                {
                    "playerid", "url[]", "Float:posX = 0.0", "Float:posY = 0.0", "Float:posZ = 0.0", "Float:distance = 50.0", "usepos = 0", 
                }
            });

            Methods.Add("PlayCrimeReportForPlayer", new MethodInformations
            {
                Description = "This function plays a crime report for a player - just like in single-player when CJ commits a crime.",
                Parameters = new string[]
                {
                    "playerid", "suspectid", "crimeid", 
                }
            });

            Methods.Add("PlayerPlaySound", new MethodInformations
            {
                Description = "Plays the specified sound for a player.",
                Parameters = new string[]
                {
                    "playerid", "soundid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("PlayerSpectatePlayer", new MethodInformations
            {
                Description = "Makes a player spectate (watch) another player.",
                Parameters = new string[]
                {
                    "playerid", "targetplayerid", "mode = SPECTATE_MODE_NORMAL", 
                }
            });

            Methods.Add("PlayerSpectateVehicle", new MethodInformations
            {
                Description = "Sets a player to spectate another vehicle. Their camera will be attached to the vehicle as if they are driving it.",
                Parameters = new string[]
                {
                    "playerid", "targetvehicleid", "mode = SPECTATE_MODE_NORMAL", 
                }
            });

            Methods.Add("PlayerTextDrawAlignment", new MethodInformations
            {
                Description = "Set the text alignment of a player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "alignment", 
                }
            });

            Methods.Add("PlayerTextDrawBackgroundColor", new MethodInformations
            {
                Description = "Adjust the background color of a player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "color", 
                }
            });

            Methods.Add("PlayerTextDrawBoxColor", new MethodInformations
            {
                Description = "Sets the color of a textdraw's box (PlayerTextDrawUseBox ).",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "color", 
                }
            });

            Methods.Add("PlayerTextDrawColor", new MethodInformations
            {
                Description = "Sets the text color of a player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "color", 
                }
            });

            Methods.Add("PlayerTextDrawDestroy", new MethodInformations
            {
                Description = "Destroy a player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", 
                }
            });

            Methods.Add("PlayerTextDrawFont", new MethodInformations
            {
                Description = "Change the font of a player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "font", 
                }
            });

            Methods.Add("PlayerTextDrawHide", new MethodInformations
            {
                Description = "Hide a player-textdraw from the player it was created for.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", 
                }
            });

            Methods.Add("PlayerTextDrawLetterSize", new MethodInformations
            {
                Description = "Sets the width and height of the letters in a player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "Float:x", "Float:y", 
                }
            });

            Methods.Add("PlayerTextDrawSetOutline", new MethodInformations
            {
                Description = "Set the outline of a player-textdraw. The outline colour cannot be changed unless PlayerTextDrawBackgroundColor is used.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "size", 
                }
            });

            Methods.Add("PlayerTextDrawSetPreviewModel", new MethodInformations
            {
                Description = "Sets a player textdraw 2D preview sprite of a specified model ID.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "modelindex", 
                }
            });

            Methods.Add("PlayerTextDrawSetPreviewRot", new MethodInformations
            {
                Description = "Sets the rotation and zoom of a 3D model preview player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "Float:fRotX", "Float:fRotY", "Float:fRotZ", "Float:fZoom", 
                }
            });

            Methods.Add("PlayerTextDrawSetPreviewVehCol", new MethodInformations
            {
                Description = "Set the color of a vehicle in a player-textdraw model preview (if a vehicle is shown).",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "color1", "color2", 
                }
            });

            Methods.Add("PlayerTextDrawSetProportional", new MethodInformations
            {
                Description = "Appears to scale text spacing to a proportional ratio. Useful when using PlayerTextDrawLetterSize to ensure the text has even character spacing.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "set", 
                }
            });

            Methods.Add("PlayerTextDrawSetSelectable", new MethodInformations
            {
                Description = "Toggles whether a player-textdraw can be selected or not.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "set", 
                }
            });

            Methods.Add("PlayerTextDrawSetShadow", new MethodInformations
            {
                Description = "Adds a shadow to the bottom-right side of the text in a player-textdraw. The shadow font matches the text font.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "size", 
                }
            });

            Methods.Add("PlayerTextDrawSetString", new MethodInformations
            {
                Description = "Change the text of a player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "string[]", 
                }
            });

            Methods.Add("PlayerTextDrawShow", new MethodInformations
            {
                Description = "Show a player-textdraw to the player it was created for.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", 
                }
            });

            Methods.Add("PlayerTextDrawTextSize", new MethodInformations
            {
                Description = "Change the size of a player-textdraw (box if PlayerTextDrawUseBox is enabled and/or clickable area for use with PlayerTextDrawSetSelectable).",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "Float:x", "Float:y", 
                }
            });

            Methods.Add("PlayerTextDrawUseBox", new MethodInformations
            {
                Description = "Toggle the box on a player-textdraw.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText:text", "use", 
                }
            });

            Methods.Add("print", new MethodInformations
            {
                Description = "Prints a string to the server console (not in-game chat) and logs (server_log.txt).",
                Parameters = new string[]
                {
                    "const string[]", 
                }
            });

            Methods.Add("printf", new MethodInformations
            {
                Description = "Outputs a formatted string on the console (the server window, not the in-game chat).",
                Parameters = new string[]
                {
                    "const format[]", "{Float", "_}:...", 
                }
            });

            Methods.Add("PutPlayerInVehicle", new MethodInformations
            {
                Description = "Puts a player in a vehicle.",
                Parameters = new string[]
                {
                    "playerid", "vehicleid", "seatid", 
                }
            });

            Methods.Add("random", new MethodInformations
            {
                Description = "Get a pseudo-random number.",
                Parameters = new string[]
                {
                    "max", 
                }
            });

            Methods.Add("RemoveBuildingForPlayer", new MethodInformations
            {
                Description = "Removes a standard San Andreas model for a single player within a specified range.",
                Parameters = new string[]
                {
                    "playerid", "modelid", "Float:fX", "Float:fY", "Float:fZ", "Float:fRadius", 
                }
            });

            Methods.Add("RemovePlayerAttachedObject", new MethodInformations
            {
                Description = "Remove an attached object from a player.",
                Parameters = new string[]
                {
                    "playerid", "index", 
                }
            });

            Methods.Add("RemovePlayerFromVehicle", new MethodInformations
            {
                Description = "Removes/ejects a player from their vehicle.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("RemovePlayerMapIcon", new MethodInformations
            {
                Description = "Removes a map icon that was set earlier for a player using SetPlayerMapIcon.",
                Parameters = new string[]
                {
                    "playerid", "iconid", 
                }
            });

            Methods.Add("RemoveVehicleComponent", new MethodInformations
            {
                Description = "Remove a component from a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", "componentid", 
                }
            });

            Methods.Add("RepairVehicle", new MethodInformations
            {
                Description = "Fully repairs a vehicle, including visual damage (bumps, dents, scratches, popped tires etc.).",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("ResetPlayerMoney", new MethodInformations
            {
                Description = "Reset a player's money to $0.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("ResetPlayerWeapons", new MethodInformations
            {
                Description = "Removes all weapons from a player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("SelectObject", new MethodInformations
            {
                Description = "Display the cursor and allow the player to select an object. OnPlayerSelectObject is called when the player selects an object.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("SelectTextDraw", new MethodInformations
            {
                Description = "Display the cursor and allow the player to select a textdraw.",
                Parameters = new string[]
                {
                    "playerid", "hovercolor", 
                }
            });

            Methods.Add("SendClientMessage", new MethodInformations
            {
                Description = "This function sends a message to a specific player with a chosen color in the chat. The whole line in the chatbox will be in the set color unless colour embedding is used (0.3c or later).",
                Parameters = new string[]
                {
                    "playerid", "color", "const message[]", 
                }
            });

            Methods.Add("SendClientMessageToAll", new MethodInformations
            {
                Description = "Displays a message in chat to all players. This is a multi-player equivalent of SendClientMessage.",
                Parameters = new string[]
                {
                    "color", "const message[]", 
                }
            });

            Methods.Add("SendDeathMessage", new MethodInformations
            {
                Description = "Adds a death to the 'killfeed' on the right-hand side of the screen for all players.",
                Parameters = new string[]
                {
                    "killer", "killee", "weapon", 
                }
            });

            Methods.Add("SendDeathMessageToPlayer", new MethodInformations
            {
                Description = "Adds a death to the 'killfeed' on the right-hand side of the screen for a single player.",
                Parameters = new string[]
                {
                    "playerid", "killer", "killee", "weapon", 
                }
            });

            Methods.Add("SendPlayerMessageToAll", new MethodInformations
            {
                Description = "Sends a message in the name of a player to all other players on the server. The line will start with the sender's name in their color, followed by the message in white.",
                Parameters = new string[]
                {
                    "senderid", "const message[]", 
                }
            });

            Methods.Add("SendPlayerMessageToPlayer", new MethodInformations
            {
                Description = "Sends a message in the name of a player to another player on the server. The message will appear in the chat box but can only be seen by the user specified with 'playerid'. The line will start with the sender's name in their color, followed by the message in white.",
                Parameters = new string[]
                {
                    "playerid", "senderid", "const message[]", 
                }
            });

            Methods.Add("SendRconCommand", new MethodInformations
            {
                Description = "Sends an RCON command.",
                Parameters = new string[]
                {
                    "command[]", 
                }
            });

            Methods.Add("SetCameraBehindPlayer", new MethodInformations
            {
                Description = "Restore the camera to a place behind the player, after using a function like SetPlayerCameraPos.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("SetDeathDropAmount", new MethodInformations
            {
                Description = "This function does not return any specific values.",
                Parameters = new string[]
                {
                    "amount", 
                }
            });

            Methods.Add("SetDisabledWeapons", new MethodInformations
            {
                Description = "Forbids weapons.",
                Parameters = new string[]
                {
                    "...", 
                }
            });

            Methods.Add("SetGameModeText", new MethodInformations
            {
                Description = "Set the name of the game mode, which appears in the server browser.",
                Parameters = new string[]
                {
                    "const string[]", 
                }
            });

            Methods.Add("SetGravity", new MethodInformations
            {
                Description = "Set the gravity for all players.",
                Parameters = new string[]
                {
                    "Float:gravity", 
                }
            });

            Methods.Add("SetMenuColumnHeader", new MethodInformations
            {
                Description = "Sets the caption of a column in a menu.",
                Parameters = new string[]
                {
                    "menuid", "column", "text[]", 
                }
            });

            Methods.Add("SetNameTagDrawDistance", new MethodInformations
            {
                Description = "Set the maximum distance to display the names of players.",
                Parameters = new string[]
                {
                    "Float:distance", 
                }
            });

            Methods.Add("SetObjectMaterial", new MethodInformations
            {
                Description = "Replace the texture of an object with the texture from another model in the game.",
                Parameters = new string[]
                {
                    "objectid", "materialindex", "modelid", "txdname[]", "texturename[]", "materialcolor", 
                }
            });

            Methods.Add("SetObjectMaterialText", new MethodInformations
            {
                Description = "Replace the texture of an object with text.",
                Parameters = new string[]
                {
                    "objectid", "text[]", "materialindex = 0", "materialsize = OBJECT_MATERIAL_SIZE_256x128", "fontface[] = \"Arial\"", "fontsize = 24", "bold = 1", "fontcolor = 0xFFFFFFFF", "backcolor = 0", "textalignment = 0", 
                }
            });

            Methods.Add("SetObjectPos", new MethodInformations
            {
                Description = "Change the position of an object.",
                Parameters = new string[]
                {
                    "objectid", "Float:X", "Float:Y", "Float:Z", 
                }
            });

            Methods.Add("SetObjectRot", new MethodInformations
            {
                Description = "Set the rotation of an object on the three axes (X, Y and Z).",
                Parameters = new string[]
                {
                    "objectid", "Float:RotX", "Float:RotY", "Float:RotZ", 
                }
            });

            Methods.Add("SetPVarFloat", new MethodInformations
            {
                Description = "Set a float player variable's value.",
                Parameters = new string[]
                {
                    "playerid", "varname[]", "Float:float_value", 
                }
            });

            Methods.Add("SetPVarInt", new MethodInformations
            {
                Description = "Set an integer player variable.",
                Parameters = new string[]
                {
                    "playerid", "varname[]", "int_value", 
                }
            });

            Methods.Add("SetPVarString", new MethodInformations
            {
                Description = "Saves a string into a player variable.",
                Parameters = new string[]
                {
                    "playerid", "varname[]", "string_value[]", 
                }
            });

            Methods.Add("SetPlayerAmmo", new MethodInformations
            {
                Description = "Set the ammo of a player's weapon.",
                Parameters = new string[]
                {
                    "playerid", "weaponslot", "ammo", 
                }
            });

            Methods.Add("SetPlayerArmedWeapon", new MethodInformations
            {
                Description = "Sets which weapon (that a player already has) the player is holding.",
                Parameters = new string[]
                {
                    "playerid", "weaponid", 
                }
            });

            Methods.Add("SetPlayerArmour", new MethodInformations
            {
                Description = "Set a player's armor level.",
                Parameters = new string[]
                {
                    "playerid", "Float:armour", 
                }
            });

            Methods.Add("SetPlayerAttachedObject", new MethodInformations
            {
                Description = "Attach an object to a specific bone on a player.",
                Parameters = new string[]
                {
                    "playerid", "index", "modelid", "bone", "Float:fOffsetX", "Float:fOffsetY", "Float:fOffsetZ", "Float:fRotX", "Float:fRotY", "Float:fRotZ", "Float:fScaleX", "Float:fScaleY", "Float:fScaleZ", "materialcolor1", "materialcolor2", 
                }
            });

            Methods.Add("SetPlayerCameraLookAt", new MethodInformations
            {
                Description = "Set the direction a player's camera looks at. Generally meant to be used in combination with SetPlayerCameraPos.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "Float:z", "cut &#61; CAMERA_CUT", 
                }
            });

            Methods.Add("SetPlayerCameraPos", new MethodInformations
            {
                Description = "Sets the camera to a specific position for a player.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("SetPlayerChatBubble", new MethodInformations
            {
                Description = "Creates a chat bubble above a player's name tag.",
                Parameters = new string[]
                {
                    "playerid", "text[]", "color", "Float:drawdistance", "expiretime", 
                }
            });

            Methods.Add("SetPlayerCheckpoint", new MethodInformations
            {
                Description = "Sets a checkpoint (red cylinder) for a player. Also shows a red blip on the radar. When players enter a checkpoint, OnPlayerEnterCheckpoint is called and actions can be performed.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "Float:z", "Float:size", 
                }
            });

            Methods.Add("SetPlayerColor", new MethodInformations
            {
                Description = "Set the colour of a player's nametag and marker (radar blip).",
                Parameters = new string[]
                {
                    "playerid", "color", 
                }
            });

            Methods.Add("SetPlayerDrunkLevel", new MethodInformations
            {
                Description = "Sets the drunk level of a player which makes the player's camera sway and vehicles hard to control.",
                Parameters = new string[]
                {
                    "playerid", "level", 
                }
            });

            Methods.Add("SetPlayerFacingAngle", new MethodInformations
            {
                Description = "Set a player's facing angle (Z rotation).",
                Parameters = new string[]
                {
                    "playerid", "Float:ang", 
                }
            });

            Methods.Add("SetPlayerFightingStyle", new MethodInformations
            {
                Description = "Set a player's special fighting style. To use in-game, aim and press the 'secondary attack' key (ENTER by default).",
                Parameters = new string[]
                {
                    "playerid", "style", 
                }
            });

            Methods.Add("SetPlayerHealth", new MethodInformations
            {
                Description = "Set the health of a player.",
                Parameters = new string[]
                {
                    "playerid", "Float:health", 
                }
            });

            Methods.Add("SetPlayerHoldingObject", new MethodInformations
            {
                Description = "Attaches an object to a bone.",
                Parameters = new string[]
                {
                    "playerid", "modelid", "bone", "Float:fOffsetX", "Float:fOffsetY", "Float:fOffsetZ", "Float:fRotX", "Float:fRotY", "Float:fRotZ", 
                }
            });

            Methods.Add("SetPlayerInterior", new MethodInformations
            {
                Description = "Set a player's interior. A list of currently known interiors and their positions can be found here.",
                Parameters = new string[]
                {
                    "playerid", "interiorid", 
                }
            });

            Methods.Add("SetPlayerMapIcon", new MethodInformations
            {
                Description = "This function allows you to place your own icons on the map, enabling you to emphasise the locations of banks, airports or whatever else you want. A total of 63 icons are available in GTA: San Andreas, all of which can be used using this function. You can also specify the color of the icon, which allows you to change the square icon (ID: 0).",
                Parameters = new string[]
                {
                    "playerid", "iconid", "Float:x", "Float:y", "Float:z", "markertype", "color", "style", 
                }
            });

            Methods.Add("SetPlayerMarkerForPlayer", new MethodInformations
            {
                Description = "Change the colour of a player's nametag and radar blip for another player.",
                Parameters = new string[]
                {
                    "playerid", "showplayerid", "color", 
                }
            });

            Methods.Add("SetPlayerName", new MethodInformations
            {
                Description = "Sets the name of a player.",
                Parameters = new string[]
                {
                    "playerid", "name[]", 
                }
            });

            Methods.Add("SetPlayerObjectMaterial", new MethodInformations
            {
                Description = "Replace the texture of a player-object with the texture from another model in the game.",
                Parameters = new string[]
                {
                    "playerid", "objectid", "materialindex", "modelid", "txdname[]", "texturename[]", "materialcolor", 
                }
            });

            Methods.Add("SetPlayerObjectMaterialText", new MethodInformations
            {
                Description = "Replace the texture of a player object with text.",
                Parameters = new string[]
                {
                    "playerid", "objectid", "text[]", "materialindex = 0", "materialsize = OBJECT_MATERIAL_SIZE_256x128", "fontface[] = \"Arial\"", "fontsize = 24", "bold = 1", "fontcolor = 0xFFFFFFFF", "backcolor = 0", "textalignment = 0", 
                }
            });

            Methods.Add("SetPlayerObjectPos", new MethodInformations
            {
                Description = "Sets the position of a player-object to the specified coordinates.",
                Parameters = new string[]
                {
                    "playerid", "objectid", "Float:X", "Float:Y", "Float:Z", 
                }
            });

            Methods.Add("SetPlayerObjectRot", new MethodInformations
            {
                Description = "Rotates an object in all directions.",
                Parameters = new string[]
                {
                    "playerid", "objectid", "Float:RotX", "Float:RotY", "Float:RotZ", 
                }
            });

            Methods.Add("SetPlayerPos", new MethodInformations
            {
                Description = "Set a player's position.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("SetPlayerPosFindZ", new MethodInformations
            {
                Description = "This sets the players position then adjusts the players z-coordinate to the nearest solid ground under the position.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("SetPlayerRaceCheckpoint", new MethodInformations
            {
                Description = "Creates a race checkpoint. When the player enters it, the OnPlayerEnterRaceCheckpoint callback is called.",
                Parameters = new string[]
                {
                    "playerid", "type", "Float:x", "Float:y", "Float:z", "Float:nextx", "Float:nexty", "Float:nextz", "Float:size", 
                }
            });

            Methods.Add("SetPlayerScore", new MethodInformations
            {
                Description = "Set a player's score. Players' scores are shown in the scoreboard (shown by holding the TAB key).",
                Parameters = new string[]
                {
                    "playerid", "score", 
                }
            });

            Methods.Add("SetPlayerShopName", new MethodInformations
            {
                Description = "Loads or unloads an interior script for a player (for example the ammunation menu).",
                Parameters = new string[]
                {
                    "playerid", "shopname[]", 
                }
            });

            Methods.Add("SetPlayerSkillLevel", new MethodInformations
            {
                Description = "Set the skill level of a certain weapon type for a player.",
                Parameters = new string[]
                {
                    "playerid", "skill", "level", 
                }
            });

            Methods.Add("SetPlayerSkin", new MethodInformations
            {
                Description = "Set the skin of a player. A player's skin is their character model.",
                Parameters = new string[]
                {
                    "playerid", "skinid", 
                }
            });

            Methods.Add("SetPlayerSpecialAction", new MethodInformations
            {
                Description = "This function allows to set players special action.",
                Parameters = new string[]
                {
                    "playerid", "actionid", 
                }
            });

            Methods.Add("SetPlayerTeam", new MethodInformations
            {
                Description = "Set the team of a player.",
                Parameters = new string[]
                {
                    "playerid", "teamid", 
                }
            });

            Methods.Add("SetPlayerTime", new MethodInformations
            {
                Description = "Sets the game time for a player. If a player's clock is enabled (TogglePlayerClock) the time displayed by it will update automatically.",
                Parameters = new string[]
                {
                    "playerid", "hour", "minute", 
                }
            });

            Methods.Add("SetPlayerVelocity", new MethodInformations
            {
                Description = "Set a player's velocity on the X, Y and Z axes.",
                Parameters = new string[]
                {
                    "playerid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("SetPlayerVirtualWorld", new MethodInformations
            {
                Description = "Set the virtual world of a player. They can only see other players or vehicles that are in that same world.",
                Parameters = new string[]
                {
                    "playerid", "worldid", 
                }
            });

            Methods.Add("SetPlayerWantedLevel", new MethodInformations
            {
                Description = "Set a player's wanted level (6 brown stars under HUD).",
                Parameters = new string[]
                {
                    "playerid", "level", 
                }
            });

            Methods.Add("SetPlayerWeather", new MethodInformations
            {
                Description = "Set a player's weather. If TogglePlayerClock has been used to enable a player's clock, weather changes will interpolate (gradually change), otherwise will change instantly.",
                Parameters = new string[]
                {
                    "playerid", "weather", 
                }
            });

            Methods.Add("SetPlayerWorldBounds", new MethodInformations
            {
                Description = "Set the world boundaries for a player. Players can not go out of the boundaries (they will be pushed back in).",
                Parameters = new string[]
                {
                    "playerid", "Float:x_max", "Float:x_min", "Float:y_max", "Float:y_min", 
                }
            });

            Methods.Add("SetSpawnInfo", new MethodInformations
            {
                Description = "This function can be used to change the spawn information of a specific player. It allows you to automatically set someone's spawn weapons, their team, skin and spawn position, normally used in case of minigames or automatic-spawn systems. This function is more crash-safe then using SetPlayerSkin in OnPlayerSpawn and/or OnPlayerRequestClass, even though this has been fixed in 0.2.",
                Parameters = new string[]
                {
                    "playerid", "team", "skin", "Float:x", "Float:y", "Float:z", "Float:Angle", "weapon1", "weapon1_ammo", "weapon2", "weapon2_ammo", "weapon3", "weapon3_ammo", 
                }
            });

            Methods.Add("SetTeamCount", new MethodInformations
            {
                Description = "This function is used to change the amount of teams used in the gamemode. It has no obvious way of being used, but can help to indicate the number of teams used for better (more effective) internal handling. This function should only be used in the OnGameModeInit callback. Important: You can pass 2 billion here if you like, this function has no effect at all.",
                Parameters = new string[]
                {
                    "teams", 
                }
            });

            Methods.Add("SetTimer", new MethodInformations
            {
                Description = "Sets a 'timer' to call a function after some time. Can be set to repeat.",
                Parameters = new string[]
                {
                    "funcname[]", "interval", "repeating", 
                }
            });

            Methods.Add("SetTimerEx", new MethodInformations
            {
                Description = "Sets a timer to call a function after the specified interval. This variant ('Ex') can pass parameters (such as a player ID) to the function.",
                Parameters = new string[]
                {
                    "funcname[]", "interval", "repeating", "const format[]", "{Float", "_}:...", 
                }
            });

            Methods.Add("SetVehicleAngularVelocity", new MethodInformations
            {
                Description = "Sets the angular X, Y and Z velocity of a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("SetVehicleHealth", new MethodInformations
            {
                Description = "Set a vehicle's health. When a vehicle's health decreases the engine will produce smoke, and finally fire when it decreases to less than 250 (25%).",
                Parameters = new string[]
                {
                    "vehicleid", "Float:health", 
                }
            });

            Methods.Add("SetVehicleNumberPlate", new MethodInformations
            {
                Description = "Set a vehicle numberplate.",
                Parameters = new string[]
                {
                    "vehicleid", "numberplate[]", 
                }
            });

            Methods.Add("SetVehicleParamsEx", new MethodInformations
            {
                Description = "Sets a vehicle's parameters for all players.",
                Parameters = new string[]
                {
                    "vehicleid", "engine", "lights", "alarm", "doors", "bonnet", "boot", "objective", 
                }
            });

            Methods.Add("SetVehicleParamsForPlayer", new MethodInformations
            {
                Description = "Set the parameters of a vehicle for a player.",
                Parameters = new string[]
                {
                    "vehicleid", "playerid", "objective", "doorslocked", 
                }
            });

            Methods.Add("SetVehiclePos", new MethodInformations
            {
                Description = "Set a vehicle's position.",
                Parameters = new string[]
                {
                    "vehicleid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("SetVehicleToRespawn", new MethodInformations
            {
                Description = "Sets a vehicle back to the position at where it was created.",
                Parameters = new string[]
                {
                    "vehicleid", 
                }
            });

            Methods.Add("SetVehicleVelocity", new MethodInformations
            {
                Description = "Sets the X, Y and Z velocity of a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", "Float:x", "Float:y", "Float:z", 
                }
            });

            Methods.Add("SetVehicleVirtualWorld", new MethodInformations
            {
                Description = "Sets the 'virtual world' of a vehicle. Players will only be able to see vehicles in their own virtual world.",
                Parameters = new string[]
                {
                    "vehicleid", "worldid", 
                }
            });

            Methods.Add("SetVehicleZAngle", new MethodInformations
            {
                Description = "Set the Z rotation (yaw) of a vehicle.",
                Parameters = new string[]
                {
                    "vehicleid", "&amp;Float:z_angle", 
                }
            });

            Methods.Add("SetWeather", new MethodInformations
            {
                Description = "Set the world weather for all players.",
                Parameters = new string[]
                {
                    "weatherid", 
                }
            });

            Methods.Add("SetWorldTime", new MethodInformations
            {
                Description = "Sets the world time (for all players) to a specific hour.",
                Parameters = new string[]
                {
                    "hour", 
                }
            });

            Methods.Add("setarg", new MethodInformations
            {
                Description = "Set an argument that was passed to a function.",
                Parameters = new string[]
                {
                    "arg", "index=0", "value", 
                }
            });

            Methods.Add("setproperty", new MethodInformations
            {
                Description = "Add a new property or change an existing property.",
                Parameters = new string[]
                {
                    "id=0", "const name[]=\"\"", "value=cellmin", "const string[]=\"\"", 
                }
            });

            Methods.Add("ShowMenuForPlayer", new MethodInformations
            {
                Description = "Shows a previously created menu for a player.",
                Parameters = new string[]
                {
                    "menuid", "playerid", 
                }
            });

            Methods.Add("ShowNameTags", new MethodInformations
            {
                Description = "Toggle the drawing of nametags, health bars and armor bars above players.",
                Parameters = new string[]
                {
                    "enabled", 
                }
            });

            Methods.Add("ShowPlayerDialog", new MethodInformations
            {
                Description = "Shows the player a synchronous (only one at a time) dialog box.",
                Parameters = new string[]
                {
                    "playerid", "dialogid", "style", "caption[]", "info[]", "button1[]", "button2[]", 
                }
            });

            Methods.Add("ShowPlayerMarkers", new MethodInformations
            {
                Description = "Toggles player markers (blips on the radar). Must be used when the server starts (OnGameModeInit). For other times, see SetPlayerMarkerForPlayer.",
                Parameters = new string[]
                {
                    "mode", 
                }
            });

            Methods.Add("ShowPlayerNameTagForPlayer", new MethodInformations
            {
                Description = "This functions allows you to toggle the drawing of player nametags, healthbars and armor bars which display above their head. For use of a similar function like this on a global level, ShowNameTags function.",
                Parameters = new string[]
                {
                    "playerid", "showplayerid", "show", 
                }
            });

            Methods.Add("SpawnPlayer", new MethodInformations
            {
                Description = "(Re)Spawns a player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("StartRecordingPlayerData", new MethodInformations
            {
                Description = "Starts recording a player's movements to a file, which can then be reproduced by an NPC.",
                Parameters = new string[]
                {
                    "playerid", "recordtype", "recordname[]", 
                }
            });

            Methods.Add("StopAudioStreamForPlayer", new MethodInformations
            {
                Description = "Stops the current audio stream for a player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("StopObject", new MethodInformations
            {
                Description = "Stop a moving object after MoveObject has been used.",
                Parameters = new string[]
                {
                    "objectid", 
                }
            });

            Methods.Add("StopPlayerHoldingObject", new MethodInformations
            {
                Description = "Removes attached objects.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("StopPlayerObject", new MethodInformations
            {
                Description = "Stop a moving player-object after MovePlayerObject has been used.",
                Parameters = new string[]
                {
                    "playerid", "objectid", 
                }
            });

            Methods.Add("StopRecordingPlayerData", new MethodInformations
            {
                Description = "Stops all the recordings that had been started with StartRecordingPlayerData for a specific player.",
                Parameters = new string[]
                {
                    "playerid", 
                }
            });

            Methods.Add("strcat", new MethodInformations
            {
                Description = "This function concatenates (joins together) two strings into the destination string.",
                Parameters = new string[]
                {
                    "dest[]", "const source[]", "maxlength = sizeof dest", 
                }
            });

            Methods.Add("strcmp", new MethodInformations
            {
                Description = "Compares two strings to see if they are the same.",
                Parameters = new string[]
                {
                    "const string1[]", "const string2[]", "bool:ignorecase", "length", 
                }
            });

            Methods.Add("strdel", new MethodInformations
            {
                Description = "Delete part of a string.",
                Parameters = new string[]
                {
                    "string[]", "start", "end", 
                }
            });

            Methods.Add("strfind", new MethodInformations
            {
                Description = "Search for a sub string in a string.",
                Parameters = new string[]
                {
                    "const string[]", "const sub[]", "bool:ignorecase=false", "pos=0", 
                }
            });

            Methods.Add("strins", new MethodInformations
            {
                Description = "Insert a string into another string.",
                Parameters = new string[]
                {
                    "string[]", "const substr[]", "pos", "maxlength=sizeof string", 
                }
            });

            Methods.Add("strlen", new MethodInformations
            {
                Description = "Get the length of a string.",
                Parameters = new string[]
                {
                    "const string[]", 
                }
            });

            Methods.Add("strmid", new MethodInformations
            {
                Description = "Extract a range of characters from a string.",
                Parameters = new string[]
                {
                    "dest[]", "const source[]", "start", "end", "maxlength=sizeof dest", 
                }
            });

            Methods.Add("strpack", new MethodInformations
            {
                Description = "Pack a string. Packed strings use 75% less memory.",
                Parameters = new string[]
                {
                    "dest[]", "const source[]", "maxlength=sizeof dest", 
                }
            });

            Methods.Add("strunpack", new MethodInformations
            {
                Description = "This function can be used to unpack a string.",
                Parameters = new string[]
                {
                    "dest[]", "const source[]", "maxlength = sizeofdest", 
                }
            });

            Methods.Add("strval", new MethodInformations
            {
                Description = "Convert a string to an integer.",
                Parameters = new string[]
                {
                    "const string[]", 
                }
            });

            Methods.Add("TextDrawAlignment", new MethodInformations
            {
                Description = "Set the alignment of text in a text draw.",
                Parameters = new string[]
                {
                    "Text:text", "alignment", 
                }
            });

            Methods.Add("TextDrawBackgroundColor", new MethodInformations
            {
                Description = "Adjusts the text draw area background color (the outline/shadow - NOT the box. For box color, see TextDrawBoxColor).",
                Parameters = new string[]
                {
                    "Text:text", "color", 
                }
            });

            Methods.Add("TextDrawBoxColor", new MethodInformations
            {
                Description = "Adjusts the text box colour (only used if TextDrawUseBox 'use' parameter is 1).",
                Parameters = new string[]
                {
                    "Text:text", "color", 
                }
            });

            Methods.Add("TextDrawColor", new MethodInformations
            {
                Description = "Sets the text color of a textdraw.",
                Parameters = new string[]
                {
                    "Text:text", "color", 
                }
            });

            Methods.Add("TextDrawCreate", new MethodInformations
            {
                Description = "Creates a textdraw. Textdraws are, as the name implies, text (mainly - there can be boxes, sprites and model previews (skins/vehicles/weapons/objects too) that is drawn on a player's screens. See this page for extensive information about textdraws.",
                Parameters = new string[]
                {
                    "Float:x", "Float:y", "text[]", 
                }
            });

            Methods.Add("TextDrawDestroy", new MethodInformations
            {
                Description = "Destroys a previously-created textdraw.",
                Parameters = new string[]
                {
                    "Text:text", 
                }
            });

            Methods.Add("TextDrawFont", new MethodInformations
            {
                Description = "Changes the text font.",
                Parameters = new string[]
                {
                    "Text:text", "font", 
                }
            });

            Methods.Add("TextDrawHideForAll", new MethodInformations
            {
                Description = "Hides a text draw for all players.",
                Parameters = new string[]
                {
                    "Text:text", 
                }
            });

            Methods.Add("TextDrawHideForPlayer", new MethodInformations
            {
                Description = "Hides a textdraw for a specific player.",
                Parameters = new string[]
                {
                    "playerid", "Text:text", 
                }
            });

            Methods.Add("TextDrawLetterSize", new MethodInformations
            {
                Description = "Sets the width and height of the letters.",
                Parameters = new string[]
                {
                    "Text:text", "Float:x", "Float:y", 
                }
            });

            Methods.Add("TextDrawSetOutline", new MethodInformations
            {
                Description = "Sets the thickness of a textdraw's text's outline. TextDrawBackgroundColor can be used to change the color.",
                Parameters = new string[]
                {
                    "Text:text", "size", 
                }
            });

            Methods.Add("TextDrawSetPreviewModel", new MethodInformations
            {
                Description = "Set the model for a textdraw model preview. Click here to see this function's effect.",
                Parameters = new string[]
                {
                    "Text:text", "modelindex", 
                }
            });

            Methods.Add("TextDrawSetPreviewRot", new MethodInformations
            {
                Description = "Sets the rotation and zoom of a 3D model preview textdraw.",
                Parameters = new string[]
                {
                    "Text:text", "Float:fRotX", "Float:fRotY", "Float:fRotZ", "Float:fZoom", 
                }
            });

            Methods.Add("TextDrawSetPreviewVehCol", new MethodInformations
            {
                Description = "If a vehicle model is used in a 3D preview textdraw, this sets the two colour values for that vehicle.",
                Parameters = new string[]
                {
                    "Text:text", "color1", "color2", 
                }
            });

            Methods.Add("TextDrawSetProportional", new MethodInformations
            {
                Description = "Appears to scale text spacing to a proportional ratio. Useful when using TextDrawLetterSize to ensure the text has even character spacing.",
                Parameters = new string[]
                {
                    "Text:text", "set", 
                }
            });

            Methods.Add("TextDrawSetSelectable", new MethodInformations
            {
                Description = "Sets the text draw to be selectable 1 or not 0.",
                Parameters = new string[]
                {
                    "Text:text", "set", 
                }
            });

            Methods.Add("TextDrawSetShadow", new MethodInformations
            {
                Description = "Sets the size of a textdraw's text's shadow.",
                Parameters = new string[]
                {
                    "Text:text", "size", 
                }
            });

            Methods.Add("TextDrawSetString", new MethodInformations
            {
                Description = "Changes the text on a textdraw.",
                Parameters = new string[]
                {
                    "Text:text", "string[]", 
                }
            });

            Methods.Add("TextDrawShowForAll", new MethodInformations
            {
                Description = "Shows a textdraw for all players.",
                Parameters = new string[]
                {
                    "Text:text", 
                }
            });

            Methods.Add("TextDrawShowForPlayer", new MethodInformations
            {
                Description = "Shows a textdraw for a specific player.",
                Parameters = new string[]
                {
                    "playerid", "Text:text", 
                }
            });

            Methods.Add("TextDrawTextSize", new MethodInformations
            {
                Description = "Change the size of a textdraw (box if TextDrawUseBox is enabled and/or clickable area for use with TextDrawSetSelectable).",
                Parameters = new string[]
                {
                    "Text:text", "Float:x", "Float:y", 
                }
            });

            Methods.Add("TextDrawUseBox", new MethodInformations
            {
                Description = "Toggle whether a textdraw uses a box or not.",
                Parameters = new string[]
                {
                    "Text:text", "use", 
                }
            });

            Methods.Add("tickcount", new MethodInformations
            {
                Description = "This function can be used as a replacement for GetTickCount, as it returns the number of milliseconds since the start-up of the server.",
                Parameters = new string[]
                {
                    "&amp;granularity=0", 
                }
            });

            Methods.Add("TogglePlayerClock", new MethodInformations
            {
                Description = "Toggle the in-game clock (top-right corner) for a specific player. When this is enabled, time will progress at 1 minute per second. Weather will also interpolate (slowly change over time) when set using SetWeather/SetPlayerWeather.",
                Parameters = new string[]
                {
                    "playerid", "toggle", 
                }
            });

            Methods.Add("TogglePlayerControllable", new MethodInformations
            {
                Description = "Toggles whether a player can control their character or not. The player will also be unable to move their camera.",
                Parameters = new string[]
                {
                    "playerid", "toggle", 
                }
            });

            Methods.Add("TogglePlayerSpectating", new MethodInformations
            {
                Description = "Toggle whether a player is in spectator mode or not. While in spectator mode a player can spectate (watch) other players and vehicles. After using this function, either PlayerSpectatePlayer or PlayerSpectateVehicle needs to be used.",
                Parameters = new string[]
                {
                    "playerid", "toggle", 
                }
            });

            Methods.Add("tolower", new MethodInformations
            {
                Description = "This function changes a single character to lowercase.",
                Parameters = new string[]
                {
                    "c", 
                }
            });

            Methods.Add("toupper", new MethodInformations
            {
                Description = "This function changes a single character to uppercase.",
                Parameters = new string[]
                {
                    "c", 
                }
            });

            Methods.Add("UnBlockIpAddress", new MethodInformations
            {
                Description = "Unblock an IP address that was previously blocked using BlockIpAddress.",
                Parameters = new string[]
                {
                    "ip_address[]", 
                }
            });

            Methods.Add("Update3DTextLabelText", new MethodInformations
            {
                Description = "Updates a 3D Text Label text and color.",
                Parameters = new string[]
                {
                    "Text3D:id", "color", "text[]", 
                }
            });

            Methods.Add("UpdatePlayer3DTextLabelText", new MethodInformations
            {
                Description = "Updates a player 3D Text Label's text and color.",
                Parameters = new string[]
                {
                    "playerid", "PlayerText3D:id", "color", "text[]", 
                }
            });

            Methods.Add("UpdateVehicleDamageStatus", new MethodInformations
            {
                Description = "Sets the various visual damage statuses of a vehicle, such as popped tires, broken lights and damaged panels.",
                Parameters = new string[]
                {
                    "vehicleid", "panels", "doors", "lights", "tires", 
                }
            });

            Methods.Add("UsePlayerPedAnims", new MethodInformations
            {
                Description = "Uses standard player walking animation (animation of the CJ skin) instead of custom animations for every skin (e.g. skating for skater skins).",
                Parameters = new string[]
                {
                    "This function has no parameters.", 
                }
            });

            Methods.Add("Uudecode", new MethodInformations
            {
                Description = "Decode an UU-encoded string.",
                Parameters = new string[]
                {
                    "dest[]", "const source[]", "maxlength=sizeof dest", 
                }
            });

            Methods.Add("valstr", new MethodInformations
            {
                Description = "Convert an integer into a string.",
                Parameters = new string[]
                {
                    "dest[]", "value", "bool:pack=false", 
                }
            });

            Methods.Add("VectorSize", new MethodInformations
            {
                Description = "Returns the norm (length) of the provided vector.",
                Parameters = new string[]
                {
                    "Float:X", "Float:Y", "Float:Z", 
                }
            });

            return Methods;
        }

        /// <summary>
        /// Format the method for insight.
        /// </summary>
        /// <param name="MethodName">Name of the method.</param>
        /// <param name="Method">Pass the current informations from MethodInformations class</param>
        /// <returns>Formated method.</returns>
        public static string FormatProvider(string MethodName, MethodInformations Method)
        {
            string Result = null;

            Result = String.Format("public {0}({1}){2}{3}{4}", MethodName, Method.Parameters != null ? String.Join(", ", Method.Parameters) : String.Empty, "\n", Method.Description, Environment.NewLine + Environment.NewLine);

            return Result;
        }
    }
}
