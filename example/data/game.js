lx.Initialize('example'); lx.Smoothing(false);var Scene0 = new lx.Scene(function() {
AMOUNT_OF_SPRITES = 5; CUR_SPRITES = 0;
function ON_SPRITE_LOAD() { CUR_SPRITES++;if (CUR_SPRITES === AMOUNT_OF_SPRITES) {


let cachedGround = new lx.Canvas(960, 960);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 320, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 384, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(128, 0, 64, 64), 384, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 384, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 384, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 384, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 384, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 384, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(128, 0, 64, 64), 384, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 384, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 448, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 448, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(64, 0, 64, 64), 448, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 448, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 448, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(192, 128, 64, 64), 448, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(320, 128, 64, 64), 448, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(192, 192, 64, 64), 448, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 448, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 512, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 512, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 512, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 512, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 512, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(256, 128, 64, 64), 512, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(192, 320, 64, 64), 512, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(320, 320, 64, 64), 512, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 512, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 576, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 576, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(64, 0, 64, 64), 576, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 576, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 576, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 576, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(256, 128, 64, 64), 576, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(256, 192, 64, 64), 576, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 576, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 448, 64, 64), 640, 256, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 640, 320, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 640, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 128, 64, 64), 640, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 0, 64, 64), 640, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(128, 0, 64, 64), 640, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 640, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(128, 0, 64, 64), 640, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 640, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 640, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 640, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 448, 64, 64), 704, 192, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 704, 256, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 704, 320, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 704, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 128, 64, 64), 704, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 0, 64, 64), 704, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 704, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 704, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(64, 0, 64, 64), 704, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 704, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 704, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 704, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 448, 64, 64), 768, 192, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 768, 256, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 768, 320, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 768, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 128, 64, 64), 768, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 0, 64, 64), 768, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 768, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 768, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 768, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 768, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 768, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 768, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 448, 64, 64), 832, 256, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 832, 320, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(512, 0, 64, 64), 832, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 128, 64, 64), 832, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 0, 64, 64), 832, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 832, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 832, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 832, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 832, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(64, 0, 64, 64), 832, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 832, 896, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 384, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 768, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 832, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(0, 0, 64, 64), 896, 896, 64, 64);cachedGround = new lx.Sprite(cachedGround); let Ground = lx.GAME.ADD_LAYER_DRAW_EVENT(0, function() {if (cachedGround.RENDER != undefined && cachedGround.Clip != undefined)lx.DrawSprite(cachedGround, -640, -640, 960, 960); });



let cachedMask = new lx.Canvas(896, 896);cachedMask.DrawSprite(Tileset1.Clip(1024, 704, 64, 64), 384, 768, 64, 64);cachedMask.DrawSprite(Tileset0.Clip(192, 64, 64, 64), 512, 448, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(448, 64, 64, 64), 640, 384, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(960, 704, 64, 64), 640, 768, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(448, 64, 64, 64), 704, 320, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(384, 0, 64, 64), 704, 384, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(512, 128, 64, 64), 704, 512, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(512, 64, 64, 64), 768, 320, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(384, 0, 64, 64), 768, 384, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(384, 64, 64, 64), 768, 512, 64, 64);cachedMask.DrawSprite(Tileset0.Clip(256, 128, 64, 64), 768, 640, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(512, 64, 64, 64), 832, 384, 64, 64);cachedMask.DrawSprite(Tileset1.Clip(1024, 704, 64, 64), 832, 640, 64, 64);cachedMask = new lx.Sprite(cachedMask); let Mask = lx.GAME.ADD_LAYER_DRAW_EVENT(1, function() {if (cachedMask.RENDER != undefined && cachedMask.Clip != undefined)lx.DrawSprite(cachedMask, -640, -640, 896, 896); });


new lx.Collider(-384, -256, 64, 384, true);new lx.Collider(-384, 128, 64, 64, true);new lx.Collider(-384, 192, 64, 64, true);new lx.Collider(-384, 256, 64, 64, true);new lx.Collider(-320, -320, 320, 64, true);new lx.Collider(-320, 320, 640, 64, true);new lx.Collider(256, -320, 64, 64, true);new lx.Collider(320, -256, 64, 384, true);new lx.Collider(320, 128, 64, 64, true);new lx.Collider(320, 192, 64, 64, true);new lx.Collider(320, 256, 64, 64, true);
let cachedCollider = new lx.Canvas(896, 896);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 256, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 320, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 384, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 448, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 512, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 576, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 640, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 704, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 128, 64, 64), 128, 768, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 192, 192, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 192, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 256, 192, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 256, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 320, 192, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 320, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 384, 192, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 384, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 448, 192, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 448, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 512, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 576, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 640, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 704, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 768, 192, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 768, 832, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 256, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 320, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 384, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 448, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 512, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 576, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 640, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 704, 64, 64);cachedCollider.DrawSprite(Tileset1.Clip(768, 0, 64, 64), 832, 768, 64, 64);cachedCollider = new lx.Sprite(cachedCollider); let Collider = lx.GAME.ADD_LAYER_DRAW_EVENT(3, function() {if (cachedCollider.RENDER != undefined && cachedCollider.Clip != undefined)lx.DrawSprite(cachedCollider, -512, -512, 896, 896); });

var PlayerCollider = new lx.Collider(8, 8, 32, 38, false, function(data) {});
PlayerCollider.Solid(true); 
PlayerCollider.Enable(); 
var FirewoodCollider = new lx.Collider(14, 16, 24, 24, false, function(data) {//First we want to get the colliding GameObject
//for further collision handling.
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
});
FirewoodCollider.Solid(true); 
FirewoodCollider.Enable(); 
var DoorCollider = new lx.Collider(64, -128, 64, 26, true, function(data) {//First we want to get the colliding GameObject
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
}});
DoorCollider.Solid(false); 
DoorCollider.Enable(); 
var Player = new lx.GameObject(PlayerSprite, -120, 0, 48, 48); 
Player.ApplyCollider(PlayerCollider); 
Player.Show(2); 
var Firewood = new lx.GameObject(FirewoodSprite, 100, 100, 48, 48); 
Firewood.ApplyCollider(FirewoodCollider); 
Firewood.Show(1); 
var FireEmitter = new lx.Emitter(FireSprite, 26, 25, 18, 25); 
FireEmitter.Setup(-0.75, 0.75, -2, 0.25, 4, 16); 
FireEmitter.Speed(8); 
FireEmitter.Show(3); 
//We first want to focus our Player GameObject.
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




//Try to restore the scene to
//it's previous state if possible!

Scene0.Restore();
}};
var Tileset0 = new lx.Sprite('res/tileset0.png', ON_SPRITE_LOAD); 
var Tileset1 = new lx.Sprite('res/tileset1.png', ON_SPRITE_LOAD); 
var PlayerSprite = new lx.Sprite('res/player.png', ON_SPRITE_LOAD); 
PlayerSprite.Clip(0, 0, 64, 64); 
var FirewoodSprite = new lx.Sprite('res/tileset1.png', ON_SPRITE_LOAD); 
FirewoodSprite.Clip(384, 768, 64, 64); 
var FireSprite = new lx.Sprite('res/fire.png', ON_SPRITE_LOAD); 
FireSprite.Rotation(0.785398163397448); });var Scene1 = new lx.Scene(function() {
AMOUNT_OF_SPRITES = 5; CUR_SPRITES = 0;
function ON_SPRITE_LOAD() { CUR_SPRITES++;if (CUR_SPRITES === AMOUNT_OF_SPRITES) {


let cachedGround = new lx.Canvas(832, 832);cachedGround.DrawSprite(Tileset1.Clip(448, 128, 64, 64), 512, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 512, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 512, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 512, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 128, 64, 64), 576, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 576, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 576, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 576, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 0, 64, 64), 576, 704, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 128, 64, 64), 640, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 640, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 640, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 640, 640, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(448, 128, 64, 64), 704, 448, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 704, 512, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 704, 576, 64, 64);cachedGround.DrawSprite(Tileset1.Clip(384, 256, 64, 64), 704, 640, 64, 64);cachedGround = new lx.Sprite(cachedGround); let Ground = lx.GAME.ADD_LAYER_DRAW_EVENT(0, function() {if (cachedGround.RENDER != undefined && cachedGround.Clip != undefined)lx.DrawSprite(cachedGround, -640, -640, 832, 832); });



let cachedMask = new lx.Canvas(704, 704);cachedMask.DrawSprite(Tileset1.Clip(384, 128, 64, 64), 640, 512, 64, 64);cachedMask = new lx.Sprite(cachedMask); let Mask = lx.GAME.ADD_LAYER_DRAW_EVENT(0, function() {if (cachedMask.RENDER != undefined && cachedMask.Clip != undefined)lx.DrawSprite(cachedMask, -640, -640, 704, 704); });



let cachedMask2 = new lx.Canvas(704, 704);cachedMask2.DrawSprite(Tileset1.Clip(384, 512, 64, 64), 640, 512, 64, 64);cachedMask2 = new lx.Sprite(cachedMask2); let Mask2 = lx.GAME.ADD_LAYER_DRAW_EVENT(1, function() {if (cachedMask2.RENDER != undefined && cachedMask2.Clip != undefined)lx.DrawSprite(cachedMask2, -640, -640, 704, 704); });

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
}});
ExitCollider.Solid(false); 
ExitCollider.Enable(); 
var PlayerCollider = new lx.Collider(16, 8, 32, 42, false, function(data) {});
PlayerCollider.Solid(true); 
PlayerCollider.Enable(); 
var TableCollider = new lx.Collider(2, -123, 60, 38, true, function(data) {});
TableCollider.Solid(true); 
TableCollider.Enable(); 
var ChairCollider = new lx.Collider(16, 18, 28, 26, false, function(data) {});
ChairCollider.Solid(true); 
ChairCollider.Enable(); 
var Player = new lx.GameObject(PlayerSprite, -64, -10, 64, 64); 
Player.ApplyCollider(PlayerCollider); 
Player.Show(2); 
var Chair = new lx.GameObject(ChairSprite, -64, -100, 64, 64); 
Chair.ApplyCollider(ChairCollider); 
Chair.Show(1); 
var FireEmitter = new lx.Emitter(FireSprite, 22, -128, 10, 14); 
FireEmitter.Setup(-0.25, 0.25, -1, 0.25, 2, 5); 
FireEmitter.Speed(8); 
FireEmitter.Show(2); 
//Once again we focus our Player GameObject.
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

//Try to restore the scene to
//it's previous state if possible!

Scene1.Restore();
}};
var Tileset0 = new lx.Sprite('res/tileset0.png', ON_SPRITE_LOAD); 
var Tileset1 = new lx.Sprite('res/tileset1.png', ON_SPRITE_LOAD); 
var FireSprite = new lx.Sprite('res/fire.png', ON_SPRITE_LOAD); 
var PlayerSprite = new lx.Sprite('res/player.png', ON_SPRITE_LOAD); 
PlayerSprite.Clip(64, 0, 64, 64); 
var ChairSprite = new lx.Sprite('res/tileset1.png', ON_SPRITE_LOAD); 
ChairSprite.Clip(512, 512, 64, 64); });lx.LoadScene(Scene0);lx.Start(60)