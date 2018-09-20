lx.Initialize('stargazing'); lx.Smoothing(false); lx.Start(60);var StarSprite = new lx.Sprite('res/star.png');function Star() {
	this.x = Math.round(-Math.random()*lx.GetDimensions().width+Math.random()*lx.GetDimensions().width);
	this.y = Math.round(-Math.random()*lx.GetDimensions().height+Math.random()*lx.GetDimensions().height);
	this.z = Math.round(Math.random()*lx.GetDimensions().width);

	this.Draw = function(gfx) {
		var sx = map_range(this.x / this.z, 0, 1, 0, lx.GetDimensions().width);
		var sy = map_range(this.y / this.z, 0, 1, 0, lx.GetDimensions().height);
		var r = map_range(this.z, 0, lx.GetDimensions().width, 16, 0);

		lx.DrawSprite(StarSprite, sx, sy, r, r);
	};

	this.Update = function() {
		this.z-=10;

		if (this.z < 1) {
			this.z = lx.GetDimensions().width;

			this.x = Math.round(-Math.random()*lx.GetDimensions().width+Math.random()*lx.GetDimensions().width);
			this.y = Math.round(-Math.random()*lx.GetDimensions().height+Math.random()*lx.GetDimensions().height);

		}
	};
}

function map_range(value, low1, high1, low2, high2) {
    return low2 + (high2 - low2) * (value - low1) / (high1 - low1);
}

var stars = [];
for (var i = 0; i < 800; i++) stars[i] = new Star();

lx.OnLayerDraw(0, function(gfx) {
	gfx.save();
	gfx.translate(lx.GetDimensions().width/2, lx.GetDimensions().height/2);

	for (var i = 0; i < stars.length; i++)
		stars[i].Draw(gfx);

	gfx.restore();
});

lx.Loops(function() {
	for (var i = 0; i < stars.length; i++) 
		stars[i].Update();
});

