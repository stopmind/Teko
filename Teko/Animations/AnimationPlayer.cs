namespace Teko.Animations;

public class AnimationPlayer
{
    public float ElapsedTime { get; private set; }
    public Animation? Animation { get; private set; }
    

    public void Play(Animation animation)
    {
        Animation = animation;
        ElapsedTime = 0;
    }

    public void Process(float delta)
    {
        if (Animation == null)
            return;
        
        ElapsedTime += delta;
        Animation.Update(ElapsedTime);

        if (ElapsedTime < Animation.Duration) return;
        ElapsedTime = Animation.Duration;
        Animation = null;
    }

    public void Stop()
    {
        Animation = null;
    }
}