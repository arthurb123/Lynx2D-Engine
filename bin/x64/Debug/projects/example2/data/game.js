lx.Initialize('example2'); lx.Smoothing(false); lx.Start(60);
var Tileset = new lx.Sprite('res/tileset.png'); 
var PlayerSprite = new lx.Sprite('res/player.png'); 
PlayerSprite.Clip(0, 0, 64, 64); 
var Chair = new lx.Sprite('res/tileset.png'); 
Chair.Clip(512, 512, 64, 64); 
var Sprite20 = new lx.Sprite('res/lynx2d/sprite.png'); 
var Sprite24 = new lx.Sprite('res/blaze.png'); 
Sprite24.Rotation(0.785398163397448); 
var Ground = lx.GAME.ADD_LAYER_DRAW_EVENT(1, function(gfx) {lx.DrawSprite(Tileset.Clip(192, 256, 64, 64),-256, -256, 64, 64);lx.DrawSprite(Tileset.Clip(320, 192, 64, 64),-256, -192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 192, 64, 64),-256, -128, 64, 64);lx.DrawSprite(Tileset.Clip(320, 192, 64, 64),-256, -64, 64, 64);lx.DrawSprite(Tileset.Clip(320, 192, 64, 64),-256, 0, 64, 64);lx.DrawSprite(Tileset.Clip(320, 192, 64, 64),-256, 64, 64, 64);lx.DrawSprite(Tileset.Clip(320, 192, 64, 64),-256, 128, 64, 64);lx.DrawSprite(Tileset.Clip(320, 192, 64, 64),-256, 192, 64, 64);lx.DrawSprite(Tileset.Clip(192, 320, 64, 64),-256, 256, 64, 64);lx.DrawSprite(Tileset.Clip(320, 320, 64, 64),-192, -256, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-192, -192, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-192, -128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-192, -64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-192, 0, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-192, 64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-192, 128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-192, 192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 256, 64, 64),-192, 256, 64, 64);lx.DrawSprite(Tileset.Clip(320, 320, 64, 64),-128, -256, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-128, -192, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-128, -128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-128, -64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 384, 64, 64),-128, 0, 64, 64);lx.DrawSprite(Tileset.Clip(0, 448, 64, 64),-128, 64, 64, 64);lx.DrawSprite(Tileset.Clip(64, 0, 64, 64),-128, 128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-128, 192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 256, 64, 64),-128, 256, 64, 64);lx.DrawSprite(Tileset.Clip(320, 320, 64, 64),-64, -256, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-64, -192, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-64, -128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-64, -64, 64, 64);lx.DrawSprite(Tileset.Clip(64, 384, 64, 64),-64, 0, 64, 64);lx.DrawSprite(Tileset.Clip(64, 576, 64, 64),-64, 64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-64, 128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),-64, 192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 256, 64, 64),-64, 256, 64, 64);lx.DrawSprite(Tileset.Clip(512, 0, 64, 64),0, -384, 64, 64);lx.DrawSprite(Tileset.Clip(512, 0, 64, 64),0, -320, 64, 64);lx.DrawSprite(Tileset.Clip(320, 320, 64, 64),0, -256, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),0, -192, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),0, -128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),0, -64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),0, 0, 64, 64);lx.DrawSprite(Tileset.Clip(128, 64, 64, 64),0, 64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),0, 128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),0, 192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 256, 64, 64),0, 256, 64, 64);lx.DrawSprite(Tileset.Clip(512, 0, 64, 64),64, -448, 64, 64);lx.DrawSprite(Tileset.Clip(512, 0, 64, 64),64, -384, 64, 64);lx.DrawSprite(Tileset.Clip(320, 320, 64, 64),64, -256, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),64, -192, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),64, -128, 64, 64);lx.DrawSprite(Tileset.Clip(64, 64, 64, 64),64, -64, 64, 64);lx.DrawSprite(Tileset.Clip(64, 64, 64, 64),64, 0, 64, 64);lx.DrawSprite(Tileset.Clip(64, 192, 64, 64),64, 64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),64, 128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),64, 192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 256, 64, 64),64, 256, 64, 64);lx.DrawSprite(Tileset.Clip(512, 0, 64, 64),128, -448, 64, 64);lx.DrawSprite(Tileset.Clip(512, 0, 64, 64),128, -384, 64, 64);lx.DrawSprite(Tileset.Clip(320, 320, 64, 64),128, -256, 64, 64);lx.DrawSprite(Tileset.Clip(128, 0, 64, 64),128, -192, 64, 64);lx.DrawSprite(Tileset.Clip(448, 0, 64, 64),128, -128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),128, -64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),128, 0, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),128, 64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),128, 128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),128, 192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 256, 64, 64),128, 256, 64, 64);lx.DrawSprite(Tileset.Clip(512, 0, 64, 64),192, -384, 64, 64);lx.DrawSprite(Tileset.Clip(512, 0, 64, 64),192, -320, 64, 64);lx.DrawSprite(Tileset.Clip(320, 320, 64, 64),192, -256, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),192, -192, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),192, -128, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),192, -64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),192, 0, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),192, 64, 64, 64);lx.DrawSprite(Tileset.Clip(0, 0, 64, 64),192, 128, 64, 64);lx.DrawSprite(Tileset.Clip(128, 0, 64, 64),192, 192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 256, 64, 64),192, 256, 64, 64);lx.DrawSprite(Tileset.Clip(256, 256, 64, 64),256, -256, 64, 64);lx.DrawSprite(Tileset.Clip(320, 128, 64, 64),256, -192, 64, 64);lx.DrawSprite(Tileset.Clip(320, 128, 64, 64),256, -128, 64, 64);lx.DrawSprite(Tileset.Clip(320, 128, 64, 64),256, -64, 64, 64);lx.DrawSprite(Tileset.Clip(320, 128, 64, 64),256, 0, 64, 64);lx.DrawSprite(Tileset.Clip(320, 128, 64, 64),256, 64, 64, 64);lx.DrawSprite(Tileset.Clip(320, 128, 64, 64),256, 128, 64, 64);lx.DrawSprite(Tileset.Clip(320, 128, 64, 64),256, 192, 64, 64);lx.DrawSprite(Tileset.Clip(256, 320, 64, 64),256, 256, 64, 64);});

var Mask = lx.GAME.ADD_LAYER_DRAW_EVENT(2, function(gfx) {lx.DrawSprite(Tileset.Clip(320, 704, 64, 64),-64, -128, 64, 64);lx.DrawSprite(Tileset.Clip(448, 448, 64, 64),0, -448, 64, 64);lx.DrawSprite(Tileset.Clip(448, 64, 64, 64),0, -320, 64, 64);lx.DrawSprite(Tileset.Clip(384, 256, 64, 64),0, -256, 64, 64);lx.DrawSprite(Tileset.Clip(384, 256, 64, 64),0, -192, 64, 64);lx.DrawSprite(Tileset.Clip(448, 0, 64, 64),0, -128, 64, 64);lx.DrawSprite(Tileset.Clip(384, 768, 64, 64),0, 128, 64, 64);lx.DrawSprite(Tileset.Clip(448, 448, 64, 64),64, -512, 64, 64);lx.DrawSprite(Tileset.Clip(448, 64, 64, 64),64, -384, 64, 64);lx.DrawSprite(Tileset.Clip(384, 0, 64, 64),64, -320, 64, 64);lx.DrawSprite(Tileset.Clip(384, 256, 64, 64),64, -256, 64, 64);lx.DrawSprite(Tileset.Clip(384, 256, 64, 64),64, -192, 64, 64);lx.DrawSprite(Tileset.Clip(512, 128, 64, 64),64, -128, 64, 64);lx.DrawSprite(Tileset.Clip(512, 448, 64, 64),128, -512, 64, 64);lx.DrawSprite(Tileset.Clip(512, 64, 64, 64),128, -384, 64, 64);lx.DrawSprite(Tileset.Clip(384, 0, 64, 64),128, -320, 64, 64);lx.DrawSprite(Tileset.Clip(384, 256, 64, 64),128, -256, 64, 64);lx.DrawSprite(Tileset.Clip(384, 256, 64, 64),128, -192, 64, 64);lx.DrawSprite(Tileset.Clip(384, 64, 64, 64),128, -128, 64, 64);lx.DrawSprite(Tileset.Clip(192, 704, 64, 64),128, 0, 64, 64);lx.DrawSprite(Tileset.Clip(512, 448, 64, 64),192, -448, 64, 64);lx.DrawSprite(Tileset.Clip(512, 64, 64, 64),192, -320, 64, 64);lx.DrawSprite(Tileset.Clip(384, 256, 64, 64),192, -256, 64, 64);lx.DrawSprite(Tileset.Clip(384, 256, 64, 64),192, -192, 64, 64);lx.DrawSprite(Tileset.Clip(448, 0, 64, 64),192, -128, 64, 64);});

var HouseCollider = new lx.Collider(0, -300, 260, 200, true, function(data) {});
HouseCollider.Solid(true); 
HouseCollider.Enable(); 
var PlayerCollider = new lx.Collider(6, 8, 36, 40, false, function(data) {});
PlayerCollider.Solid(true); 
PlayerCollider.Enable(); 
var PropCollider = new lx.Collider(18, 18, 24, 32, false, function(data) {});
PropCollider.Solid(true); 
PropCollider.Enable(); 
var FireCollider = new lx.Collider(18, 152, 32, 26, true, function(data) {if (lx.FindGameObjectWithCollider(data.trigger).Identifier() == "Player")
	Player.AddVelocity(
		Player.Movement().VX*-3,
		Player.Movement().VY*-3
				);});
FireCollider.Solid(true); 
FireCollider.Enable(); 
var StoneCollider = new lx.Collider(142, 8, 42, 36, false, function(data) {});
StoneCollider.Solid(true); 
StoneCollider.Enable(); 
var Prop2Collider = new lx.Collider(18, 18, 24, 32, false, function(data) {});
Prop2Collider.Solid(true); 
Prop2Collider.Enable(); 
var Player = new lx.GameObject(PlayerSprite, -60, 30, 48, 48); 
Player.ApplyCollider(PlayerCollider); 
Player.Show(3); 
var Prop = new lx.GameObject(Chair, -60, 120, 64, 64); 
Prop.ApplyCollider(PropCollider); 
Prop.Show(2); 
var Prop2 = new lx.GameObject(Chair, 64, 164, 64, 64); 
Prop2.ApplyCollider(Prop2Collider); 
Prop2.Show(2); 
var FireEmitter = new lx.Emitter(Sprite24, 24, 152, 18, 30); 
FireEmitter.Setup(0, 0, -1, 0, 12, 22); 
FireEmitter.Speed(8); 
FireEmitter.Show(3); 
Player.Focus();
Player.Identifier("Player");
Player.SetTopDownController(.25, .25, 2);

var cur = 0,
    standard = 8;

lx.OnKey('W', function() {
	Player.Clip().X = 64;
});

lx.OnKey('A', function() {
	Player.Clip().X = 128;
});

lx.OnKey('S', function() {
	Player.Clip().X = 0;
});

lx.OnKey('D', function() {
	Player.Clip().X = 192;
});

Player.Loops(function() {
	if (Player.Movement().VX != 0 || Player.Movement().VY != 0) {
		cur++;
		if (cur > standard) {
			cur = 0;
			Player.Clip().Y+=64;

			if (Player.Clip().Y >= 256)
				Player.Clip().Y = 0;
		}

		PlayerCollider.Enable();
	} else
		Player.Clip().Y = 0;
});

lx.OnLayerDraw(0, function(gfx) {
	gfx.drawImage(Tileset.IMG, 196, 0, 64, 64, 0, 0, lx.GetDimensions().width, lx.GetDimensions().height);
});

Prop.OnMouse(0, function() {
	PlayerCollider.Disable();

	Player.Movement(0, 0);

	Player.Position(Prop.Position().X+6,
				Prop.Position().Y-4);
});

Prop2.OnMouse(0, function() {
	PlayerCollider.Disable();

	Player.Movement(0, 0);

	Player.Position(Prop2.Position().X+6,
				Prop2.Position().Y-4);
});