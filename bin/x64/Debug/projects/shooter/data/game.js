lx.Initialize('shooter'); lx.Smoothing(false); lx.Start(60);var Sprite0 = new lx.Sprite('res/lynx2d/sprite.png'); var Bullet = new lx.Sprite('res/lynx2d/sprite.png'); var Player = new lx.GameObject(Sprite0, 0, 0, 64, 64); Player.Show(1); Player.Focus();
Player.SetTopDownController(.25, .25, 3);

var speed = 12;

lx.OnMouse(0, function(mouse) {
	Shoot(mouse);
});

lx.OnMouse(2, function() {
	Player.ClearCollider();

	Player.ApplyCollider(
		new lx.Collider(16, 16, 32, 32, false)
	);
});

lx.Loops(function() {
	if (speed > 0) speed--;
});

function Shoot(mouse) {
	if (speed > 0) return;
	speed = 12;

	var bullet = new lx.GameObject(
		Bullet, 
		Player.Position().X+16, 
		Player.Position().Y+16, 
		32, 
		32
	);

	bullet.MovementDecelerates(true);
	bullet.MaxVelocity(10);
	bullet.AddVelocity(
		(mouse.X-lx.GetDimensions().width/2)/28,
		(mouse.Y-lx.GetDimensions().height/2)/28
	);

	bullet.ApplyCollider(
		new lx.Collider(8, 8, 16, 16, false, function (data) {
			var tM = lx.FindGameObjectWithCollider(data.trigger).Movement();

			lx.FindGameObjectWithCollider(data.self).Movement(
				tM.VX,
				tM.VY
			);
		})
	);

	bullet.Show(0);
}