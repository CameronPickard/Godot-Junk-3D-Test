using Godot;
using System;

public partial class Player : CharacterBody3D
{

	public float speed = 70.0f;
	public float fall_acceleration = -50f;
	public AnimatedSprite3D animatedSprite3D;
	public Direction lastDirection = Direction.Down;
	private float _currentYvelocity = -50f;
	private static float _jumpVelocity = 100f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite3D = GetNode<AnimatedSprite3D>("AnimatedSprite3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 direction = Vector3.Zero;
		string animationName = "standing_down";
		if (Input.IsActionPressed("move_down") )
		{
			//move down
			direction.Z += 1;
			animationName = "walking_down";
			lastDirection = Direction.Down;
		}
		if(Input.IsActionPressed("move_up")) 
		{
			direction.Z -= 1;
			animationName = "walking_up";
			lastDirection = Direction.Up;
		}
		if(Input.IsActionPressed("move_right"))
		{
			direction.X += 1;
			animationName = "walking_right";
			lastDirection = Direction.Right;
		}
		if(Input.IsActionPressed("move_left")) 
		{
			direction.X -= 1;
			animationName = "walking_left";
			lastDirection = Direction.Left;
		}

		direction = direction.Normalized() * speed * (float) delta;
		if(direction.Dot(direction) != 0.0f) 
		{
		}
		else 
		{
			animationName = "standing_down";
			switch (lastDirection) 
			{
				case Direction.Down:
					animationName = "standing_down";
					break;
				case Direction.Up:
					animationName = "standing_up";
					break;
				case Direction.Right:
					animationName = "standing_right";
					break;
				case Direction.Left:
					animationName = "standing_left";
					break;
					
			}
		}
		animatedSprite3D.Animation = animationName;
		animatedSprite3D.Play();
		if (IsOnFloor() && Input.IsActionPressed("jump")) 
		{
			_currentYvelocity = _jumpVelocity;
			direction.Y += _currentYvelocity * (float)delta;
		}
		if (!IsOnFloor())
		{
			_currentYvelocity = Math.Max(_currentYvelocity - (180 * (float) delta), fall_acceleration);
			direction.Y += _currentYvelocity * (float)delta;
		}
		Velocity = direction;
		MoveAndSlide();
	}
}

public enum Direction
{
	Down,
	Up,
	Left,
	Right
}
