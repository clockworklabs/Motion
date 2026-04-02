# Motion
**Motion** is an animation library for Unity. It has support for Tween and physics-based Spring animations as well as countdowns.

To create a Tween Animation, you just need to call `DoMotion.Tween(getter, setter, target, duration)`. There are many overloads for different types and arguments. Tweens have a specific duration and follow an `Ease` curve.

To create a Spring Animation, you just need to call `DoMotion.Spring(getter, setter, target)`. Springs act like springs in the real world. This yields natural looking results.

Both Tween and Spring can be set up to repeat with the chain method `Repeat`. There are many other chain methods available, like `Delay`, `SetInitialVelocity`, `SetEase`, `OnStep`, `OnComplete` ...

To create a countdown, you just need to call `DoMotion.Countdown(duration, callback)`. The `callback` is called when the countdown reaches 0.

`DoMotion` needs to be stepped. You can manually call `DoMotion.Step(deltaTime)` or simply add the `MotionAutoStep` component to a `GameObject` in your scene.

## Springs
If you're new to Spring animations, these are great resources to start with:
- https://www.joshwcomeau.com/animation/a-friendly-introduction-to-spring-physics/
- https://blog.maximeheckel.com/posts/the-physics-behind-spring-animations/
- https://react-spring-visualizer.com/

## Installation
Installing Motion is simple. You can add the package via the Unity Package Manager using the Git URL, or by modifying your `manifest.json` file directly.

Add the following package URL: `https://github.com/clockworklabs/Motion.git#[target-version]` (latest is `1.8.0`).
