# VirtualRealityFinalProject
Virtual Reality Project<br><br>
<b>Written in C# using the Unity Game Engine. <br>
Requires the use of virtual reality equiptment, preferably the Vive. <br><br>
Basic instruction for the game:</b><br>
One of the Vive controllers is the <b>bow hand</b>, this can be on either left hand or right hand, simply swap controllers to whichever hand is more comfortable holding the bow. The game begins with an intermission, this allows the user to swap.<br>
The hand not controlling the bow is the <b>arrow hand</b>, it is used to:
<ol>
<li>Receive an arrow from the quiver (pressing the trigger close to the quiver)
<li>Lock the arrow to the bow (pressing the trigger when close to the bow)
<li>Drawing the arrow (pull away from bow while holding the trigger)
<li>Use the bow and arrow hand together to aim.
<li>Shooting the arrow (release trigger when arrow sufficiently drawn)
</ol>
The aim of the game is to survive 3 waves of mobs by preventing the mobs from entering their destination. This can be done via the use of a bow and arrows and the aid of an attacking tower and movement decrease tower.<br>

<b>Basic controls for the game:</b><br>
<b>Trigger:</b> <ul>
<b>Arrow hand:</b> <ul>
<li><b>Model: Vive Controller:</b> This is the default hand, the trigger down will only activate when colliding with the quiver. When the trigger is pressed down by the quiver, an arrow is picked up and the <b>Arrow hand’s model</b> is changed into an <b>Arrow</b>.
<li><b>Model: Arrow:</b> This only happens when an arrow is picked up. Pressing the trigger down and releasing the trigger close to the bow will lock the arrow onto the bow. Pressing and holding the trigger down again will allow the arrow to be drawn (pulled back). Vibration haptics accommodate this move and the intensity increases depending on how far the arrow is drawn. Releasing the trigger will release the arrow as well as reverting the <b>model</b> into the <b>Vive Controller</b>.
</ul>
<li><b>Bow hand:</b> Pressing trigger does nothing.
</ul>
<b>Application menu:</b> Shoots a laser and whenever the laser collides with an “end” or a slow “tower” object, teleport to it.<br>
<b>Direction Pad:</b>
<b>Touching:</b> <ul>
<li><b>Left and Right:</b> Shoots a ray out, this ray previews the location for where the tower would be placed.
<li><b>Pressing:</b> <ul>
<li><b>Left and Right:</b> Places the tower at the ray’s intersection if the collider’s tag is named “floor”. Left being an attacking tower and right being a movement decrease tower. Towers are limited to 3 in total.
<li><b>Down:</b> This brings up the score canvas that will automatically disappear in 2 seconds, and includes details such as:<ul>
<li><b>Score:</b> the amount of mobs killed by the user (excluding towers).<br>
<li><b>Life:</b> How many more mobs can pass into the goal before it is game over.<br>
<li><b>Wave:</b> Which wave the user is currently on.<br>
</ul></ul></ul>
<b>Grip:</b> Squeezing the controller’s grip will return the user to the menu.<br>
