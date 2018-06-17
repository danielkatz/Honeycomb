using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;
using XDependency.Abstractions.Extensions;
using XDependency.Tests.Fakes;
using XDependency.Tests.Fixtures;
using Xunit;

namespace XDependency.Tests
{
    public partial class DependencyObjectTests
    {
        [Fact]
        public void CanInheritValueFromParent()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.RegisterAttached("AttachedProperty", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default", isInherited: true));
                var parent = new DependencyObjectFake();
                var child = new DependencyObjectFake();
                var childInheritanceSource = child.Component.GetValueSource<InheritanceValueSource>();

                parent.SetValue(prop, "inherited");

                Assert.Equal("default", child.GetValue(prop));

                childInheritanceSource.ParentComponent = parent.Component;

                Assert.Equal("inherited", child.GetValue(prop));
            }
        }

        [Fact]
        public void RaisesPropertyChangedOnParentValueSet()
        {
            using (new DefaultImplementationFixture())
            {
                var raised = 0;

                var parent = new DependencyObjectFake();
                var child = new DependencyObjectFake();
                var childInheritanceSource = child.Component.GetValueSource<InheritanceValueSource>();

                var prop = Dependency.Property.RegisterAttached("AttachedProperty", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default", isInherited: true, propertyChangedCallback: (s, e) =>
                {
                    if (object.ReferenceEquals(s, child))
                    {
                        raised++;
                    }
                }));

                childInheritanceSource.ParentComponent = parent.Component;

                Assert.Equal("default", child.GetValue(prop));

                parent.SetValue(prop, "inherited");

                Assert.Equal("inherited", child.GetValue(prop));
                Assert.Equal(1, raised);
            }
        }

        [Fact]
        public void RaisesPropertyChangedOnParentChange()
        {
            using (new DefaultImplementationFixture())
            {
                var raised = 0;

                var parent1 = new DependencyObjectFake();
                var parent2 = new DependencyObjectFake();
                var parent3 = new DependencyObjectFake();

                var child = new DependencyObjectFake();
                var childInheritanceSource = child.Component.GetValueSource<InheritanceValueSource>();

                var prop = Dependency.Property.RegisterAttached("AttachedProperty", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default", isInherited: true, propertyChangedCallback: (s, e) =>
                {
                    if (object.ReferenceEquals(s, child))
                    {
                        raised++;
                    }
                }));

                parent1.SetValue(prop, "inherited1");
                parent2.SetValue(prop, "inherited2");
                parent3.SetValue(prop, "inherited2");

                childInheritanceSource.ParentComponent = parent1.Component;
                Assert.Equal("inherited1", child.GetValue(prop));

                childInheritanceSource.ParentComponent = parent2.Component;
                Assert.Equal("inherited2", child.GetValue(prop));

                childInheritanceSource.ParentComponent = parent3.Component;
                Assert.Equal("inherited2", child.GetValue(prop));

                childInheritanceSource.ParentComponent = null;
                Assert.Equal("default", child.GetValue(prop));

                Assert.Equal(3, raised);
            }
        }

        [Fact]
        public void WontInheritNonInheritingPropertyValues()
        {
            using (new DefaultImplementationFixture())
            {
                var parent = new DependencyObjectFake();

                var child = new DependencyObjectFake();
                var childInheritanceSource = child.Component.GetValueSource<InheritanceValueSource>();

                var inheritingProp = Dependency.Property.RegisterAttached("AttachedProperty", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default", isInherited: true));
                var nonInheritingAttached = Dependency.Property.RegisterAttached("NonInheritingAttachedProperty", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var nonInheritingMember = Dependency.Property.Register("NonInheritingMemberProperty", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));

                parent.SetValue(inheritingProp, "inherited");
                parent.SetValue(nonInheritingAttached, "inherited");
                parent.SetValue(nonInheritingMember, "inherited");

                childInheritanceSource.ParentComponent = parent.Component;

                Assert.Equal("inherited", child.GetValue(inheritingProp));
                Assert.Equal("default", child.GetValue(nonInheritingAttached));
                Assert.Equal("default", child.GetValue(nonInheritingAttached));
            }
        }

        [Fact]
        public void CanOverrideMetadataPropertyChangedCallback()
        {
            using (new DefaultImplementationFixture())
            {
                var raisedParent = new List<IDependencyObject>();
                var raisedChild = new List<IDependencyObject>();

                var parent = new DependencyObjectFake();
                var child = new SecondDependencyObjectFake();

                var prop = Dependency.Property.RegisterAttached("AttachedProperty", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default", isInherited: true, propertyChangedCallback: (s, e) =>
                {
                    raisedParent.Add(s);
                }));

                prop.OverrideMetadata(typeof(SecondDependencyObjectFake), new PropertyMetadata("overriden", propertyChangedCallback: (s, e) =>
                {
                    raisedChild.Add(s);
                }));

                var childInheritanceSource = child.Component.GetValueSource<InheritanceValueSource>();
                childInheritanceSource.ParentComponent = parent.Component;

                parent.SetValue(prop, "inherited");

                Assert.Collection(raisedParent,
                    x => Assert.Same(parent, x),
                    x => Assert.Same(child, x));

                Assert.Collection(raisedChild,
                    x => Assert.Same(child, x));
            }
        }

        [Fact]
        public void CanOverrideMetadataDefaultValue()
        {
            using (new DefaultImplementationFixture())
            {
                var parent = new DependencyObjectFake();
                var child = new SecondDependencyObjectFake();

                var prop = Dependency.Property.RegisterAttached("AttachedProperty", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default", isInherited: true));
                prop.OverrideMetadata(typeof(SecondDependencyObjectFake), new PropertyMetadata("overriden"));

                var childInheritanceSource = child.Component.GetValueSource<InheritanceValueSource>();
                childInheritanceSource.ParentComponent = parent.Component;

                Assert.Equal("default", parent.GetValue(prop));
                Assert.Equal("overriden", child.GetValue(prop));
            }
        }
    }
}
