lx.Initialize('emitter example'); lx.Smoothing(false); lx.Start(60);var Sprite1 = new lx.Sprite('res/blaze.png');var Sprite2 = new lx.Sprite('res/em.png');var Sprite3 = new lx.Sprite('res/matter.png');var Sprite4 = new lx.Sprite('res/jetpack.png');var emitter = new lx.Emitter(Sprite1, 0, 0, 1000, 60);

lx.ParticleLimit(1000);

emitter
	.Setup(-5, 5, -5, 5, 8, 48)
	.Speed(0)
	.MovementDecelerates(false)
	.Show(0);

var timer = 60;

lx.Loops(function() {
	timer--;

	if (timer > 0) return;

	timer = 60;

	eval("emitter.SPRITE=Sprite" + Math.round(Math.random()*3+1));
});