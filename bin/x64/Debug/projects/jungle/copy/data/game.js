lx.Initialize('jungle'); lx.Smoothing(false); lx.Start(60);var Sprite0 = new lx.Sprite('res/player.png');Sprite0.Clip(0, 0, 64, 64);var Sprite3 = new lx.Sprite('res/player.png');Sprite3.Clip(0, 0, 64, 64);var PlayerSpriteReverse = new lx.Sprite('res/player_reverse.png');PlayerSpriteReverse.Clip(0, 0, 64, 64);var WorldTile = new lx.Sprite('res/world.png');WorldTile.Clip(24, 24, 32, 32);var Player = new lx.GameObject(Sprite0, 0, 0, 128, 128);Player.Show(0);var Other = new lx.GameObject(Sprite3, -200, 0, 128, 128);Other.Show(0);Player.Focus();
Player.SetSideWaysController(.2, 2);

var frame = 0, cur = 0, moving = 0, direction = 0;
var standard = 10;

var playerEmitter = new lx.Emitter(new lx.Sprite('res/world.png', 32, 26, 8, 8), 64, 92, 5, 20);
playerEmitter.Speed(12);
playerEmitter.Follows(Player);

lx.Loops(function() {	
	if (Math.abs(Player.Movement().VX) > .5) {
		moving = 1;
		playerEmitter.Position(54+direction*20, 92);
		playerEmitter.Setup(-.5, .5, 0, -.5, 2, 6);
		playerEmitter.Emit(1, 1);

		if (Player.Movement().VX > 0) direction = 0;
		else direction = 1;
	}
	else {
		playerEmitter.Hide();
		moving = 0;
	}

	cur++;

	if (cur >= standard) {
		cur = 0;
		frame++;

		if (frame >= 4) frame = 0;
	}
	
	if (direction == 0) {
		Player.SPRITE = Sprite0;
		Player.Clip(frame*64, moving*64, 64, 64);
	}
	else {
		Player.SPRITE = PlayerSpriteReverse;
		Player.Clip(448-frame*64, moving*64, 64, 64);
	}

	Other.Clip(frame*64, 0, 64, 64);
});lx.OnLayerDraw(0, function() {
	for (var x = -3; x < 6; x++) {
		lx.DrawSprite(WorldTile, x*64, 96, 64, 64);
	}
});