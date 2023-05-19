//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

public class MultiTrigger : TriggerBase {
  internal new static MultiTrigger CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new MultiTrigger(cPtr, cMemoryOwn);
  }

  internal MultiTrigger(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(MultiTrigger obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public MultiTrigger() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_MultiTrigger();
  }

  public ConditionCollection Conditions {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MultiTrigger_Conditions_get(swigCPtr);
      return (ConditionCollection)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public SetterBaseCollection Setters {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.MultiTrigger_Setters_get(swigCPtr);
      return (SetterBaseCollection)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

}

}

