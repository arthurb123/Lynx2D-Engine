lx.Initialize('woah'); lx.Smoothing(false); lx.Start(60);var Star = new lx.Sprite('res/star.png');var stars = [];
var speed = 1;
var standard = 2;
var counter = standard;

function AddStar() {
	for (var i = 0; i < stars.length+1; i++) {
		if (stars[i] == undefined) {
			stars[i] = {
				x: Math.random()*lx.GetDimensions().width,
				y: Math.random()*lx.GetDimensions().height,
				w: 10,
				h: 10,
				a: 1
			};
			break;
		}
	}
}

function HandleStars() {
	for (var i = 0; i < stars.length; i++) {
		if (stars[i] != undefined) {
			stars[i].w+=speed;
			stars[i].h+=speed;
	
			if (stars[i].w >= lx.GetDimensions().width/6) {
				stars[i].a-=speed/65;

				if (stars[i].a <= 0) {
					stars[i] = undefined;
				}
			}
		}
	}
}

lx.Loops(function() {
	counter++;

	if (counter >= standard) {
		counter = 0;
		AddStar();
	}

	HandleStars();
});

lx.OnLayerDraw(0, function(gfx) {
	stars.forEach(function(star) {
		if (star != undefined) {
			gfx.save();
			gfx.globalAlpha = star.a;
			lx.DrawSprite(Star, star.x-star.w/2, star.y-star.h/2, star.w, star.h);
			gfx.restore();
		}
	});
});