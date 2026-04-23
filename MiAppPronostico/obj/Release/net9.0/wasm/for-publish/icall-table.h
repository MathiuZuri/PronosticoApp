#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
187,
198,
199,
200,
201,
202,
203,
204,
205,
206,
209,
210,
323,
324,
325,
353,
354,
355,
382,
383,
384,
501,
502,
503,
506,
540,
541,
543,
545,
547,
549,
554,
562,
563,
564,
565,
566,
567,
568,
569,
570,
661,
670,
671,
740,
746,
749,
751,
756,
757,
759,
760,
764,
765,
767,
768,
771,
772,
773,
776,
778,
781,
783,
785,
794,
861,
863,
865,
875,
876,
877,
879,
885,
886,
887,
888,
889,
897,
898,
899,
903,
904,
906,
908,
1123,
1308,
1309,
7495,
7496,
7498,
7499,
7500,
7501,
7502,
7504,
7505,
7506,
7523,
7525,
7530,
7532,
7534,
7536,
7587,
7588,
7590,
7591,
7592,
7593,
7594,
7596,
7598,
8589,
8593,
8595,
8596,
8597,
8598,
9025,
9026,
9027,
9028,
9046,
9047,
9048,
9092,
9157,
9160,
9168,
9169,
9170,
9171,
9172,
9466,
9470,
9471,
9501,
9537,
9544,
9551,
9562,
9565,
9590,
9672,
9674,
9684,
9686,
9687,
9688,
9695,
9710,
9730,
9731,
9739,
9741,
9748,
9749,
9752,
9754,
9759,
9765,
9766,
9773,
9775,
9787,
9790,
9791,
9792,
9803,
9813,
9819,
9820,
9821,
9823,
9824,
9841,
9843,
9858,
9878,
9879,
9904,
9909,
9939,
9940,
10496,
10582,
10583,
10799,
10800,
10808,
10809,
10810,
10816,
10881,
11304,
11305,
11496,
11501,
11511,
12337,
12358,
12360,
12362,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_ModF (double,int);
int ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw (int,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw (int,int,int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
int ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_Debugger_IsAttached_internal ();
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 187,
ves_icall_System_Array_InternalCreate,
// token 198,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 199,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 200,
ves_icall_System_Array_CanChangePrimitive,
// token 201,
ves_icall_System_Array_FastCopy,
// token 202,
ves_icall_System_Array_GetLengthInternal_raw,
// token 203,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 204,
ves_icall_System_Array_GetGenericValue_icall,
// token 205,
ves_icall_System_Array_GetValueImpl_raw,
// token 206,
ves_icall_System_Array_SetGenericValue_icall,
// token 209,
ves_icall_System_Array_SetValueImpl_raw,
// token 210,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 323,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 324,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 325,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 353,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 354,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 355,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 382,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 383,
ves_icall_System_Enum_InternalGetCorElementType,
// token 384,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 501,
ves_icall_System_Environment_get_ProcessorCount,
// token 502,
ves_icall_System_Environment_get_TickCount,
// token 503,
ves_icall_System_Environment_get_TickCount64,
// token 506,
ves_icall_System_Environment_FailFast_raw,
// token 540,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 541,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 543,
ves_icall_System_GC_SuppressFinalize_raw,
// token 545,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 547,
ves_icall_System_GC_GetGCMemoryInfo,
// token 549,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 554,
ves_icall_System_Object_MemberwiseClone_raw,
// token 562,
ves_icall_System_Math_Ceiling,
// token 563,
ves_icall_System_Math_Cos,
// token 564,
ves_icall_System_Math_Floor,
// token 565,
ves_icall_System_Math_Log10,
// token 566,
ves_icall_System_Math_Pow,
// token 567,
ves_icall_System_Math_Sin,
// token 568,
ves_icall_System_Math_Sqrt,
// token 569,
ves_icall_System_Math_Tan,
// token 570,
ves_icall_System_Math_ModF,
// token 661,
ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw,
// token 670,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 671,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 740,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 746,
ves_icall_RuntimeType_make_array_type_raw,
// token 749,
ves_icall_RuntimeType_make_byref_type_raw,
// token 751,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 756,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 757,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 759,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 760,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 764,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 765,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 767,
ves_icall_System_RuntimeType_getFullName_raw,
// token 768,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 771,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 772,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 773,
ves_icall_RuntimeType_GetFields_native_raw,
// token 776,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 778,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 781,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 783,
ves_icall_RuntimeType_GetName_raw,
// token 785,
ves_icall_RuntimeType_GetNamespace_raw,
// token 794,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 861,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 863,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 865,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 875,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 876,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 877,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 879,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 885,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 886,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 887,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 888,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 889,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 897,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 898,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 899,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 903,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 904,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 906,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 908,
ves_icall_System_String_FastAllocateString_raw,
// token 1123,
ves_icall_System_Type_internal_from_handle_raw,
// token 1308,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1309,
ves_icall_System_ValueType_Equals_raw,
// token 7495,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 7496,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 7498,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 7499,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 7500,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 7501,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 7502,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 7504,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 7505,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 7506,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 7523,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 7525,
mono_monitor_exit_icall_raw,
// token 7530,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 7532,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 7534,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 7536,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 7587,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 7588,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 7590,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 7591,
ves_icall_System_Threading_Thread_GetState_raw,
// token 7592,
ves_icall_System_Threading_Thread_SetState_raw,
// token 7593,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 7594,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 7596,
ves_icall_System_Threading_Thread_YieldInternal,
// token 7598,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 8589,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 8593,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 8595,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 8596,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 8597,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 8598,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 9025,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 9026,
ves_icall_System_GCHandle_InternalFree_raw,
// token 9027,
ves_icall_System_GCHandle_InternalGet_raw,
// token 9028,
ves_icall_System_GCHandle_InternalSet_raw,
// token 9046,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 9047,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 9048,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 9092,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 9157,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 9160,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 9168,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 9169,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 9170,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 9171,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 9172,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw,
// token 9466,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 9470,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 9471,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 9501,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 9537,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 9544,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 9551,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 9562,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 9565,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 9590,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 9672,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 9674,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 9684,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 9686,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 9687,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 9688,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 9695,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 9710,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 9730,
ves_icall_reflection_get_token_raw,
// token 9731,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 9739,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 9741,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 9748,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 9749,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 9752,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 9754,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 9759,
ves_icall_reflection_get_token_raw,
// token 9765,
ves_icall_get_method_info_raw,
// token 9766,
ves_icall_get_method_attributes,
// token 9773,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 9775,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 9787,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 9790,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 9791,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 9792,
ves_icall_reflection_get_token_raw,
// token 9803,
ves_icall_InternalInvoke_raw,
// token 9813,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 9819,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 9820,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 9821,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 9823,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 9824,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 9841,
ves_icall_InvokeClassConstructor_raw,
// token 9843,
ves_icall_InternalInvoke_raw,
// token 9858,
ves_icall_reflection_get_token_raw,
// token 9878,
ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw,
// token 9879,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 9904,
ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw,
// token 9909,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 9939,
ves_icall_reflection_get_token_raw,
// token 9940,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 10496,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 10582,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 10583,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 10799,
ves_icall_ModuleBuilder_basic_init_raw,
// token 10800,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 10808,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 10809,
ves_icall_ModuleBuilder_getToken_raw,
// token 10810,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 10816,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 10881,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 11304,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 11305,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 11496,
ves_icall_System_Diagnostics_Debugger_IsAttached_internal,
// token 11501,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 11511,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 12337,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 12358,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 12360,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 12362,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
0,
0,
0,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
};
