# Motion
Animation library for Unity. It has support for Tween and Spring animations as well as countdowns.

To create a Tween Animation, you just need to call `DoMotion.Tween(getter, setter, target, duration)`. There are many overloads for different types and arguments. Tweens have a specific duration and follow an `Ease` curve.

To create a Spring Animation, you just need to call `DoMotion.Spring(getter, setter, target)`. Springs act physically accurate to how springs work in the real world. It gives natural looking results.

Both Tween and Spring can be set up to repeat with the chain method `Repeat`. There are many other chain methods available, like `Delay`, `SetInitialVelocity`, `SetEase`, `OnStep`, `OnComplete` ...

To create a countdown, you just need to call `DoMotion.Countdown(duration, callback)`. The `callback` is called when the countdown reaches 0.

`DoMotion` needs to be stepped. You can manually call `DoMotion.Step(deltaTime)` or simply add the `MotionAutoStep` component to a `GameObject` in your scene.