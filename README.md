# Saving-Sistem-for-unity
A simple state saving script for unity 2d games(may work on 3d as well with some modifications)

Hey, this is a quick and simplified guide on how to implement this code in your game!

SETUP INSTRUCTIONS:

STEP 1:
  Think about your game and figure out what parameters of what type of objects you want to save and which ones you don`t.

STEP 2:
  If you need more classes other than Player and Npc, create a subclass for it in the CharData.cs class, add the parameters you want to save and then add it    to the "Data" class definition on the end of the file.
  For exemple: Let`s if you want to add an Item class you would do something like this:

  public class ItemData{
  public int id;
  public string name;
  public Sprite itemImage;
  public int Damage;
  public int Durability;
  }

  then, after you add it as a parameter of "Data" the Data class will look like this:

  [System.Serializable] public class Data {
    // If you want to add extra classes you want to save separately, create the classes like the others and add them here:
    public List<NPCData> npcs;
    public PlayerData player;
    public GameData gameData;
    public ItemData itemData;
  }

  After this, create a class similar to NpcCLass.cs or PlayerClass.cs so you can assign your new class to game objects.
  In this case this class will be the same as the NpcClass but instead of "public NPCData npcData;" it would be "public ItemData itemData;".

  You`ll may also need to alter the Load() function to properly load every aspect of an item object. Similarly as i did with the tranformNpc and
  tranformPlayer for those classes.

  Additionally, if you would like the data of your new class to also be saved on a separate file like Player, Npc and World data, you will need to change the
  Save() function to acomodate that.
  You'll just have to imitate what i did with the others. You're smart, you'll figure it out ;).
  

STEP 3:
  Open unity and add the "___Class" classes as components to their respective game object types. PlayerClass for player objects, NpcClass for npcs, etc...

Step 4:
  You`re basically done, now you create an instance of SaveSis on any sort of "manager" script you have on your project and call the Save() or Load()           functions as you please.

IMPORTANT NOTE:
  Goes without saying but: THE SAVE FILES CAN'T BE EMPTY WHEN LOADING.
  To avoid that, create a scene on the editor with everything on their starting state and call the Save() function to fill the data files on the right format.

USAGE TIP:
  Although this script is more likely to be used for simple "Save game" buttons, it was created because without it, if i had a scene in my game that was
  suposed to be visited multiple times but with minor changes i would need to create a clone of it with the changes i wanted and this is just not viable for
  a game with many heavy scenes. This script solves this problem and allows me program a transition to a previous scene and do any alteration i need to that
  scene.
  To do that, Before loading a new scene you can create a simple script that uses the functions i provided to change the stored data to whatever you desire.
  Let`s say you want to change the spawning location of your player and the dialog of an specifc NPC. You can change that data on the file, save and then, on
  the next scene, call Load() to apply the changes.

