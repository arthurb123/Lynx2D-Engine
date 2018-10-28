lx.Initialize('example'); lx.Smoothing(false); lx.Start(60);var Scene0 = new lx.Scene(function() {var Tileset0 = new lx.Sprite('res/tileset0.png'); var Tileset1 = new lx.Sprite('res/tileset1.png'); var PlayerSprite = new lx.Sprite('res/player.png'); PlayerSprite.Clip(0, 0, 64, 64); var FirewoodSprite = new lx.Sprite('res/tileset1.png'); FirewoodSprite.Clip(384, 768, 64, 64); var FireSprite = new lx.Sprite('res/fire.png'); FireSprite.Rotation(0.785398163397448); var Ground = lx.GAME.ADD_LAYER_DRAW_EVENT(0, function(gfx) {lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-320, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-256, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(128, 0, 64, 64),-256, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-256, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-256, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-256, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-256, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-256, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(128, 0, 64, 64),-256, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-256, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-192, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-192, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(64, 0, 64, 64),-192, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-192, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-192, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(192, 128, 64, 64),-192, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(320, 128, 64, 64),-192, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(192, 192, 64, 64),-192, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-192, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-128, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-128, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-128, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-128, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-128, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(256, 128, 64, 64),-128, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(192, 320, 64, 64),-128, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(320, 320, 64, 64),-128, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-128, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-64, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-64, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(64, 0, 64, 64),-64, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-64, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-64, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-64, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(256, 128, 64, 64),-64, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(256, 192, 64, 64),-64, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),-64, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 448, 64, 64),0, -384, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),0, -320, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),0, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 128, 64, 64),0, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 0, 64, 64),0, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(128, 0, 64, 64),0, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),0, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(128, 0, 64, 64),0, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),0, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),0, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),0, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 448, 64, 64),64, -448, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),64, -384, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),64, -320, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),64, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 128, 64, 64),64, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 0, 64, 64),64, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),64, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),64, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(64, 0, 64, 64),64, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),64, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),64, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),64, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 448, 64, 64),128, -448, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),128, -384, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),128, -320, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),128, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 128, 64, 64),128, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 0, 64, 64),128, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),128, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),128, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),128, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),128, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),128, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),128, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 448, 64, 64),192, -384, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),192, -320, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 0, 64, 64),192, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 128, 64, 64),192, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 0, 64, 64),192, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),192, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),192, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),192, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),192, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(64, 0, 64, 64),192, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),192, 256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, 192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(0, 0, 64, 64),256, 256, 64, 64);});

var Mask = lx.GAME.ADD_LAYER_DRAW_EVENT(1, function(gfx) {lx.DrawSprite(Tileset1.Rotation(0).Clip(1024, 704, 64, 64),-256, 128, 64, 64);lx.DrawSprite(Tileset0.Rotation(0).Clip(192, 64, 64, 64),-128, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 64, 64, 64),0, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(960, 704, 64, 64),0, 128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 64, 64, 64),64, -320, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 0, 64, 64),64, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 128, 64, 64),64, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 64, 64, 64),128, -320, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 0, 64, 64),128, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 64, 64, 64),128, -128, 64, 64);lx.DrawSprite(Tileset0.Rotation(0).Clip(256, 128, 64, 64),128, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(512, 64, 64, 64),192, -256, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(1024, 704, 64, 64),192, 0, 64, 64);});

var PlayerCollider = new lx.Collider(8, 8, 32, 38, false, function(data) {});PlayerCollider.Solid(true); PlayerCollider.Enable(); var FirewoodCollider = new lx.Collider(14, 16, 24, 24, false, function(data) {//First we want to get the colliding GameObject
//further collision handling.
var go = lx.FindGameObjectWithCollider(data.trigger);

//We want to make sure that we are
//dealing with a valid GameObject!
if (go == undefined) return;

//When the player is moving us too hard we want
//to burn the player to a crisp, bad player.
if (go.Identifier() == "Player" && Math.abs(go.Movement().VX) >= 2.125 ||
    go.Identifier() == "Player" && Math.abs(go.Movement().VY) >= 2.125) {

	//Here we change the sprite to a burned player tileset.
	Player.SPRITE = new lx.Sprite("res/player_burned.png").Clip(0, 0, 64, 64);

	//Let's also give him a good push!
	Player.Movement(
		Player.Movement().VX * -1,
		Player.Movement().VY * -1
		          );

}
});FirewoodCollider.Solid(true); FirewoodCollider.Enable(); var DoorCollider = new lx.Collider(64, -128, 64, 26, true, function(data) {//First we want to get the colliding GameObject
//further collision handling.
var go = lx.FindGameObjectWithCollider
(data.trigger);

//We want to make sure that we are
//dealing with a valid GameObject!
if (go == undefined) return;

//If this is the player, we want to 
//save the current scene and
//move him inside.
if (go.Identifier() == "Player") {
	//We must undertake some actions to
	//make sure the player won't get stuck
	//while transitioning.
	lx.StopKey("w");
	Player.Position().Y += 8;
	Player.Movement(0, 0);
	PlayerSprite.Clip(0, 0);

	Scene0.Save();
	lx.LoadScene(Scene1);
	Scene1.Restore();
}});DoorCollider.Solid(false); DoorCollider.Enable(); var Player = new lx.GameObject(PlayerSprite, -120, 0, 48, 48); Player.ApplyCollider(PlayerCollider); Player.Show(2); var Firewood = new lx.GameObject(FirewoodSprite, 100, 100, 48, 48); Firewood.ApplyCollider(FirewoodCollider); Firewood.Show(1); var FireEmitter = new lx.Emitter(FireSprite, 26, 25, 18, 25); FireEmitter.Setup(-0.75, 0.75, -2, 0.25, 4, 16); FireEmitter.Speed(8); FireEmitter.Show(3); //We first want to focus our Player GameObject.
//Then make it so that we can control it.
Player.Focus();
Player.SetTopDownController(.125, .125, 2.25);

//We also want to add an identifier to the
//player GameObject for later use.
Player.Identifier("Player");


//Next let's make our player animate
//when it is moving...
var playerStandard = 8,
    playerCur = 0;

//We will use a unique update loop
//for this.
lx.Loops(function() {
	playerCur++;

	//We want our player to animate
	//every 8 update frames.
	if (playerCur >= playerStandard)
		playerCur = 0;
	else 
		return;

	//The player must be moving to
	//be animated!
	if (Math.abs(Player.Movement().VX) >= .2 || 
	    Math.abs(Player.Movement().VY) >= .2) {
		Player.Clip().Y+=64;

		if (Player.Clip().Y >= 256)
			Player.Clip().Y = 0;
	} else
		Player.Clip().Y = 0;
});

//We want to make sure that our 
//Player GameObject faces the 
//correct direction.
lx.OnKey("w", function() { Player.Clip().X = 64; });
lx.OnKey("s", function() { Player.Clip().X = 0; });
lx.OnKey("a", function() { Player.Clip().X = 128; });
lx.OnKey("d", function() { Player.Clip().X = 192; });
//We want the fire emitter to follow our
//Firewood when being moved around.
FireEmitter.Follows(Firewood);

//We want colliding GameObjects not
//to be able to move within certain
//TileMap areas;

//Water colliders
new lx.Collider(-186, 68, 116, 172, true);
new lx.Collider(-68, 132, 60, 108, true);

//House colliders
new lx.Collider(0, -256, 256, 128, true);
new lx.Collider(0, -128, 64, 48, true);
new lx.Collider(128, -128, 128, 48, true);



});var Scene1 = new lx.Scene(function() {var Tileset0 = new lx.Sprite('res/tileset0.png'); var Tileset1 = new lx.Sprite('res/tileset1.png'); var FireSprite = new lx.Sprite('res/fire.png'); var PlayerSprite = new lx.Sprite('res/player.png'); PlayerSprite.Clip(64, 0, 64, 64); var ChairSprite = new lx.Sprite('res/tileset1.png'); ChairSprite.Clip(512, 512, 64, 64); var Ground = lx.GAME.ADD_LAYER_DRAW_EVENT(0, function(gfx) {lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 128, 64, 64),-128, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),-128, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),-128, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),-128, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 128, 64, 64),-64, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),-64, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),-64, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),-64, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 0, 64, 64),-64, 64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 128, 64, 64),0, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),0, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),0, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),0, 0, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(448, 128, 64, 64),64, -192, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),64, -128, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),64, -64, 64, 64);lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 256, 64, 64),64, 0, 64, 64);});

var Mask = lx.GAME.ADD_LAYER_DRAW_EVENT(0, function(gfx) {lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 128, 64, 64),0, -128, 64, 64);});

var Mask2 = lx.GAME.ADD_LAYER_DRAW_EVENT(1, function(gfx) {lx.DrawSprite(Tileset1.Rotation(0).Clip(384, 512, 64, 64),0, -128, 64, 64);});

var ExitCollider = new lx.Collider(-64, 76, 64, 64, true, function(data) {//First we want to get the colliding GameObject
//further collision handling.
var go = lx.FindGameObjectWithCollider
(data.trigger);

//We want to make sure that we are
//dealing with a valid GameObject!
if (go == undefined) return;

//If this is the player, we want to 
//move him to the previous scene
//and load the save state.
if (go.Identifier() == "Player")  {
	//We must take some action to prevent 
	//the player from getting stuck while 
	//transitioning
	lx.StopKey("s");
	Player.Position().Y -= 8;
	Player.Movement(0, 0);
	PlayerSprite.Clip(64, 0);

	Scene1.Save();
	lx.LoadScene(Scene0);
	Scene0.Restore();
}});ExitCollider.Solid(false); ExitCollider.Enable(); var PlayerCollider = new lx.Collider(16, 8, 32, 42, false, function(data) {});PlayerCollider.Solid(true); PlayerCollider.Enable(); var TableCollider = new lx.Collider(2, -123, 60, 38, true, function(data) {});TableCollider.Solid(true); TableCollider.Enable(); var ChairCollider = new lx.Collider(16, 18, 28, 26, false, function(data) {});ChairCollider.Solid(true); ChairCollider.Enable(); var Player = new lx.GameObject(PlayerSprite, -64, -10, 64, 64); Player.ApplyCollider(PlayerCollider); Player.Show(2); var Chair = new lx.GameObject(ChairSprite, -64, -100, 64, 64); Chair.ApplyCollider(ChairCollider); Chair.Show(1); var FireEmitter = new lx.Emitter(FireSprite, 22, -128, 10, 14); FireEmitter.Setup(-0.25, 0.25, -1, 0.25, 2, 5); FireEmitter.Speed(8); FireEmitter.Show(2); //Once again we focus our Player GameObject.
//Then make it so that we can control it.
Player.Focus();
Player.SetTopDownController(.25, .25, 1.5);

//And add a fitting identifier.
Player.Identifier("Player");

//Next let's make our player animate
//when it is moving...
var playerStandard = 8,
    playerCur = 0;

//We will use a unique update loop
//for this.
lx.Loops(function() {
	playerCur++;

	//We want our player to animate
	//every 8 update frames.
	if (playerCur >= playerStandard)
		playerCur = 0;
	else 
		return;

	//The player must be moving to
	//be animated!
	if (Math.abs(Player.Movement().VX) >= .2 || 
	    Math.abs(Player.Movement().VY) >= .2) {
		Player.Clip().Y+=64;

		if (Player.Clip().Y >= 256)
			Player.Clip().Y = 0;
	} else
		Player.Clip().Y = 0;
});

//We want to make sure that our 
//Player GameObject faces the 
//correct direction.
lx.OnKey("w", function() { Player.Clip().X = 64; });
lx.OnKey("s", function() { Player.Clip().X = 0; });
lx.OnKey("a", function() { Player.Clip().X = 128; });
lx.OnKey("d", function() { Player.Clip().X = 192; });
//We want to make sure any colliding
//GameObject doesn't leave the scene.
//A.K.A world colliders!

//Top collider
new lx.Collider(-128, -192, 256, 42, true);

//Middle colliders
new lx.Collider(-192, -192, 64, 256, true);
new lx.Collider(128, -192, 64, 256, true);

//Bottom colliders
new lx.Collider(-128, 50, 64, 64, true);
new lx.Collider(0, 50, 128, 64, true);
});lx.LoadScene(Scene0);