using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaApplication3
{
    using Avalonia.Animation;
    using Avalonia.Media;
    using Avalonia.Styling;
    using Avalonia;
    using Avalonia.VisualTree;
    using System.Threading;
    using Avalonia.Controls;

    public class CustomFade : IPageTransition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFade"/> class.
        /// </summary>
        public CustomFade()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFade"/> class.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        public CustomFade(TimeSpan duration)
        {
            Duration = duration;
        }

        /// <summary>
        /// Gets the duration of the animation.
        /// </summary>
        public TimeSpan Duration { get; set; }

        public async Task Start(Visual from, Visual to, bool forward,
                                                CancellationToken cancellationToken)
        {
            
                
                var tasks = new List<Task>();
                var transparentProperty = LayoutTransformControl.OpacityProperty;
                //Duration = new TimeSpan(10000000);

            if (from != null)
            {
                var animation = new Animation
                {
                    FillMode = FillMode.Forward,
                    Children =
                {
                    new KeyFrame
                    {
                        Setters = { new Setter
                        { Property = transparentProperty, Value = 1d } },
                        Cue = new Cue(0d)
                    },
                    new KeyFrame
                    {
                        Setters =
                        {
                            new Setter
                            {
                                Property = transparentProperty,
                                Value = 0d
                            }
                        },
                        Cue = new Cue(1d)
                    }
                },
                    Duration = Duration
                };
                tasks.Add(animation.RunAsync(from, cancellationToken));
                from.IsVisible = false;
            }
            

            if (to != null)
            {

                to.IsVisible = true;

                var animation = new Animation
                {
                    FillMode = FillMode.Forward,
                    Children =
                {
                    new KeyFrame
                    {
                        Setters =
                        {
                            new Setter
                            {
                                Property = transparentProperty,
                                Value = 0d
                            }
                        },
                        Cue = new Cue(0d)
                    },
                    new KeyFrame
                    {
                        Setters = { new Setter
                        {
                            Property = transparentProperty, Value = 1d
                        }},
                        Cue = new Cue(1d)
                    }
                },
                    Duration = Duration
                };
                tasks.Add(animation.RunAsync(to, cancellationToken));
            }
        }

        /// <summary>
        /// Gets the common visual parent of the two control.
        /// </summary>
        /// <param name="from">The from control.</param>
        /// <param name="to">The to control.</param>
        /// <returns>The common parent.</returns>
        /// <exception cref="ArgumentException">
        /// The two controls do not share a common parent.
        /// </exception>
        /// <remarks>
        /// Any one of the parameters may be null, but not both.
        /// </remarks>
        private static Visual GetVisualParent(Visual? from, Visual? to)
        {
            var p1 = (from ?? to)!.GetVisualParent();
            var p2 = (to ?? from)!.GetVisualParent();

            if (p1 != null && p2 != null && p1 != p2)
            {
                throw new ArgumentException(
                                    "Controls for PageSlide must have same parent.");
            }

            return p1 ?? throw new InvalidOperationException(
                                                    "Cannot determine visual parent.");
        }
    }
}
    
