﻿lx.Initialize('test'); lx.Smoothing(false); lx.Start(60);var Sprite0 = new lx.Sprite('res/lynx2d/sprite.png');var Sprite5 = new lx.Sprite('res/lynx2d/sprite.png');Sprite5.Rotation(5.58505360638185);var Sprite9 = new lx.Sprite('res/lynx2d/sprite.png');Sprite9.Rotation(0.436332312998582);var PlayerCollider = new lx.Collider(16, 16, 32, 32, false, function(data) {if (!data.static) 
	lx.FindGameObjectWithCollider(data.trigger).AddVelocity(Player.Movement().VX*1.5, Player.Movement().VY*1.5);});PlayerCollider.Disable();var OtherCollider = new lx.Collider(16, 16, 32, 32, false, function(data) {});OtherCollider.Disable();var Other2Collider = new lx.Collider(16, 16, 32, 32, false, function(data) {});Other2Collider.Disable();var Player = new lx.GameObject(Sprite0, 0, 0, 64, 64);Player.ApplyCollider(PlayerCollider);Player.Show(0);var Other = new lx.GameObject(Sprite5, -120, 0, 64, 64);Other.ApplyCollider(OtherCollider);Other.Show(0);var Other2 = new lx.GameObject(Sprite9, 120, 0, 64, 64);Other2.ApplyCollider(Other2Collider);Other2.Show(0);Player.SetTopDownController(.25, .25, 2);
Player.Focus();var text = new lx.UIText("Hello World!", 32, -8, 18);

text.Alignment("center");
text.Follows(Other);
text.Show();