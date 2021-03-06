﻿using System;
using System.Text;
using BanKai.Basic.Common;
using Xunit;

namespace BanKai.Basic
{
    // ReSharper disable ConditionIsAlwaysTrueOrFalse
    // ReSharper disable ExpressionIsAlwaysNull

    public class Inheritance
    {
        [Fact]
        public void should_access_public_memeber_in_derived_class()
        {
            var demoClass = new InheritMemberAccessDemoClass();

            // please change the variable value to fix the test.
            const string expected = "Public Property Value";

            Assert.Equal(expected, demoClass.PublicProperty); //PublicProperty是InheritMemberAccessDemoBaseClass的实例属性
        }

        [Fact]
        public void should_access_protected_member_in_derived_class()
        {
            var demoClass = new InheritMemberAccessDemoClass();

            string actualValue = demoClass.ManipulateProtectedMember();

            // please change the variable value to fix the test.            
            const string expected = "The value is Protected Property Value";

            Assert.Equal(expected, actualValue);
        }

        [Fact]
        public void should_access_member_of_most_derived_class()
        {
            var demoClass = new PolymorphismDemoClass();
            var castToBaseClass = (PolymorphismDemoClassBase)demoClass;

            string actualValue = castToBaseClass.VirtualMethod(); //基类需方法，会调用最派生的重写方法

            //most derived是相对于实例本身的类型

            // please change the variable value to fix the test.
            const string expected = "DerivedClass";

            Assert.Equal(expected, actualValue);
        }

        [Fact]
        public void should_return_casted_result_if_it_is_castable()
        {
            var demoClass = new PolymorphismDemoClass();
            var castToBaseClass = demoClass as PolymorphismDemoClassBase;  //success
            bool isNull = castToBaseClass == null;

            // please change the variable value to fix the test.
            const bool expected = false;

            Assert.Equal(expected, isNull);
        }

        [Fact]
        public void should_return_null_if_it_is_not_castable()
        {
            var demoClass = new PolymorphismDemoClass();
            object castToObject = demoClass;

            var castResult = castToObject as StringBuilder;  //faild  一条继承链上是可以成功的，其他不行
            bool isNull = castResult == null;

            // please change the variable value to fix the test.
            const bool expected = true;

            Assert.Equal(expected, isNull);
        }

        [Fact]
        public void should_throw_exception_if_it_is_not_castable()
        {
            var demoClass = new PolymorphismDemoClass();
            object castToObject = demoClass;

            // please change the variable value to fix the test.
            Type expectedExceptionType = typeof(InvalidCastException);

            Assert.NotEqual(typeof(SystemException), expectedExceptionType);
            Assert.NotEqual(typeof(Exception), expectedExceptionType);

            Assert.Throws(expectedExceptionType, () => (StringBuilder)castToObject); //StringBuilder sealed,base:object
        }

        [Fact]
        public void should_reference_to_same_object_after_casting()
        {
            var demoClass = new PolymorphismDemoClass();
            var castToBaseClass = (PolymorphismDemoClassBase)demoClass;

            bool referenceEqual = ReferenceEquals(demoClass, castToBaseClass);  //地址相同或同为null

            // please change the variable value to fix the test.
            const bool expected = true;

            Assert.Equal(expected, referenceEqual);
        }

        [Fact]
        public void should_throw_exception_when_downcasting_fail()
        {
            var demoClassBase = new PolymorphismDemoClassBase();  //base=>derived,faild

            // please change the variable value to fix the test.
            Type expectedExceptionType = typeof(InvalidCastException);

            Assert.NotEqual(typeof(SystemException), expectedExceptionType);
            Assert.NotEqual(typeof(Exception), expectedExceptionType);

            Assert.Throws(expectedExceptionType, () => (PolymorphismDemoClass)demoClassBase);
        }

        [Fact]
        public void should_be_able_to_hide_non_virtual_method_in_derived_class()
        {
            var demoClass = new HideMemberDemoClass();
            var castedToBaseClass = (HideMemberDemoClassBase)demoClass;

            string methodReturnValue = demoClass.MethodToHide();  //new关键字，告诉编译器隐藏基类中的MethodToHide(),避免警告
            string baseClassMethodReturnValue = castedToBaseClass.MethodToHide(); //new会破坏它的多态

            // please change the following 2 variable values to fix the test.
            const string expectedMethodReturnValue = "HideMemberDemoClass::MethodToHide()";
            const string expectedBaseClassMethodReturnValue = "HideMemberDemoClassBase::MethodToHide()";

            Assert.Equal(expectedMethodReturnValue, methodReturnValue);
            Assert.Equal(expectedBaseClassMethodReturnValue, baseClassMethodReturnValue);
        }

        [Fact]
        public void should_be_able_to_get_base_class_members_using_base_keyword()
        {
            var demoClass = new BaseKeywordDemoClass();

            string name = demoClass.Name;

            // please change the variable value to fix the test.
            const string expected = "BaseClass's derived class.";

            Assert.Equal(expected, name);
        }

        [Fact]
        public void should_call_default_constructors_of_base_class()
        {
            var demoClass = new InheritanceConstructorCallDemoClass();  //先执行基类无参构造函数，在执行自己的无参构造函数

            string message = demoClass.ConstructorCallMessage;

            // please change the variable value to fix the test.
            const string expected = "InheritanceConstructorCallDemoClassBase::Ctor()\r\nInheritanceConstructorCallDemoClass::Ctor()\r\n";

            Assert.Equal(expected, message);
        }

        [Fact]
        public void should_call_default_constructor_of_base_class_when_call_derived_ctor_with_args()
        {
            var demoClass = new InheritanceConstructorCallDemoClass(1);  //先执行基类无参构造函数，在执行自己的int构造函数

            string message = demoClass.ConstructorCallMessage;

            // please change the variable value to fix the test.
            const string expected = "InheritanceConstructorCallDemoClassBase::Ctor()\r\nInheritanceConstructorCallDemoClass::Ctor(int)\r\n";

            Assert.Equal(expected, message);
        }

        [Fact]
        public void should_be_able_to_specify_which_base_ctor_to_call()
        {
            var demoClass = new InheritanceConstructorCallDemoClass("1");  // : base(int.Parse(arg))

            string message = demoClass.ConstructorCallMessage;

            // please change the variable value to fix the test.
            const string expected = "InheritanceConstructorCallDemoClassBase::Ctor(int)\r\nInheritanceConstructorCallDemoClass::Ctor(string)\r\n";

            Assert.Equal(expected, message);
        }

        [Fact]
        public void should_be_able_to_specify_which_ctor_of_current_class_to_call()
        {
            var demoClass = new InheritanceConstructorCallDemoClass(1, "1"); //: this(intArg)

            string message = demoClass.ConstructorCallMessage;

            // please change the variable value to fix the test.
            const string expected = "InheritanceConstructorCallDemoClassBase::Ctor()\r\nInheritanceConstructorCallDemoClass::Ctor(int)\r\nInheritanceConstructorCallDemoClass::Ctor(int, string)\r\n";

            Assert.Equal(expected, message);
        }

        [Fact]
        public void should_be_able_to_choose_most_specific_type_when_overloading()
        {
            var demoClass = new MethodOverloadDemoClass();

            string returnValueForBaseClassOverloading =
                demoClass.Foo(new MethodOverloadBaseClass());  //base
            string returnValueForDerivedClassOverloading =
                demoClass.Foo(new MethodOverloadDerivedClass());  //derived
            string returnValueForCastingOverloading =
                demoClass.Foo((MethodOverloadBaseClass)(new MethodOverloadDerivedClass()));  //derived=>base

            const string expectedBaseClassOverloadingValue = "Foo(MethodOverloadBaseClass)";
            const string expectedDerivedClassOverloadingValue = "Foo(MethodOverloadDerivedClass)";
            const string expectedCastOverloadingValue = "Foo(MethodOverloadBaseClass)";

            Assert.Equal(expectedBaseClassOverloadingValue, returnValueForBaseClassOverloading);
            Assert.Equal(expectedDerivedClassOverloadingValue, returnValueForDerivedClassOverloading);
            Assert.Equal(expectedCastOverloadingValue, returnValueForCastingOverloading);
        }
    }

    // ReSharper restore ExpressionIsAlwaysNull
    // ReSharper restore ConditionIsAlwaysTrueOrFalse
}