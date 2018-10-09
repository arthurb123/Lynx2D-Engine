lx.Initialize('dungeon crawler'); lx.Smoothing(false); lx.Start(60);var Tileset = new lx.Sprite('res/tileset.png'); var PlayerSprite = new lx.Sprite('res/player.png'); PlayerSprite.Clip(0, 0, 64, 64); var Ground = lx.GAME.ADD_LAYER_DRAW_EVENT(0, function(gfx) {lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(48, 48, 48, 48),-288, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-288, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(48, 48, 48, 48),-240, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-240, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(48, 48, 48, 48),-192, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-192, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(48, 48, 48, 48),-144, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-144, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(3.141593).Clip(0, 0, 48, 48),-144, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-96, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-96, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-96, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(48, 96, 48, 48),-96, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 96, 48, 48),-96, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 96, 48, 48),-96, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 96, 48, 48),-96, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(96, 96, 48, 48),-96, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-96, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-96, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-96, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(3.141593).Clip(0, 0, 48, 48),-96, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),-48, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(3.141593).Clip(0, 0, 48, 48),-48, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),0, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(3.141593).Clip(0, 0, 48, 48),0, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),48, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(3.141593).Clip(0, 0, 48, 48),48, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),96, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(3.141593).Clip(0, 0, 48, 48),96, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),144, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),144, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),144, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),144, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),144, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 48, 48, 48),144, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 96, 48, 48),144, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 96, 48, 48),144, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 96, 48, 48),144, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 96, 48, 48),144, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 96, 48, 48),144, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(96, 96, 48, 48),144, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(48, 48, 48, 48),192, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),192, 240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, -288, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, -240, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, -192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, -96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(48, 48, 48, 48),240, -48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, 144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, 192, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(0, 0, 48, 48),240, 240, 48, 48);});
var Mask = lx.GAME.ADD_LAYER_DRAW_EVENT(1, function(gfx) {lx.DrawSprite(Tileset.Rotation(0).Clip(48, 0, 48, 48),-144, 0, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(144, 0, 48, 48),-48, -144, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(96, 0, 48, 48),96, 96, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(48, 0, 48, 48),192, 48, 48, 48);lx.DrawSprite(Tileset.Rotation(0).Clip(144, 0, 48, 48),192, 240, 48, 48);});
var Player = new lx.GameObject(PlayerSprite, 0, -24, 48, 48); Player.Show(2); Player.Focus();

//Movement
lx.OnKey('W', function() {
	Player.Position().Y-=48;
	Player.Clip().X = 64;
	Tick();

	lx.StopKey('W');
});

lx.OnKey('A', function() {
	Player.Position().X-=48;
	Player.Clip().X = 128;
	Tick();

	lx.StopKey('A');
});

lx.OnKey('S', function() {
	Player.Position().Y+=48;
	Player.Clip().X = 0;
	Tick();

	lx.StopKey('S');
});

lx.OnKey('D', function() {
	Player.Position().X+=48;
	Player.Clip().X = 192;
	Tick();

	lx.StopKey('D');
});var standardTicks = 200;
var curTicks = 0;

lx.Loops(function() {
	curTicks++;

	if (curTicks > standardTicks) {
		curTicks = 0;

		Tick();
	}
});

function Tick() {
	
};var size = 10;

lx.OnLayerDraw(10, function(gfx) {
	if (lx.GAME.DEBUG) return;

	gfx.save();
	gfx.globalAlpha = .85;

	gfx.fillRect(0, 0, lx.GetDimensions().width/2-size*24, lx.GetDimensions().height);
	gfx.fillRect(lx.GetDimensions().width/2+size*24, 0, lx.GetDimensions().width, lx.GetDimensions().height);
	gfx.fillRect(lx.GetDimensions().width/2-size*24, 0, size*48, lx.GetDimensions().height/2-size*24);
	gfx.fillRect(lx.GetDimensions().width/2-size*24, lx.GetDimensions().height/2+size*24, size*48, lx.GetDimensions().height);
	
	gfx.restore();
});