








namespace SFXCodeCompletion.Parser
{
	using System.Collections.Generic;

	public static partial class KnownTokens
	{
		private static Dictionary<string, SfxTokenType> GetDictionary()
		{
			var keywords = "defined location std140 std430 scalar buffer_reference bindless_sampler bindless_image bound_sampler bound_image passthrough offset component input_attachment_index num_views secondary_view_offset buffer_reference_align vertices invocations max_vertices stream index max_primitives local_size_x local_size_y local_size_z attribute const uniform varying buffer shared coherent volatile restrict readonly writeonly atomic_uint layout quads equal_spacing fractional_even_spacing fractional_odd_spacing cw ccw points centroid flat smooth noperspective patch sample break continue do for while switch case default if else subroutine in out inout true false invariant precise discard return lowp mediump highp precision struct common partition active asm class union enum typedef template this resource goto inline noinline public static extern external interface long short half fixed unsigned superp input output hvec2 hvec3 hvec4 fvec2 fvec3 fvec4 sampler3DRect filter sizeof cast namespace using constant_id local_size_x_id local_size_y_id local_size_z_id local_size_variable input_attachment_index push_constant set binding subpassInput subpassInputMS isubpassInput isubpassInputMS usubpassInput usubpassInputMS early_fragment_tests post_depth_coverage devicecoherent queuefamilycoherent workgroupcoherent subgroupcoherent nonprivate point_mode origin_upper_left pixel_center_integer early_fragment_tests early_and_late_fragment_tests_amd post_depth_coverage blend_support override_coverage viewport_relative shaderrecordnv shaderrecordext hitobjectshaderrecordnv derivative_group_quadsnv derivative_group_linearnv primitive_culling constexpr".Trim().Split();
			var functions = "radians degrees sin cos tan asin acos atan sinh cosh tanh asinh acosh atanh pow exp log exp2 log2 sqrt inversesqrt abs sign floor trunc round roundEven ceil fract mod modf min max clamp mix step smoothstep isnan isinf floatBitsToInt floatBitsToUint intBitsToFloat uintBitsToFloat fma frexp ldexp packUnorm2x16 packSnorm2x16 packUnorm4x8 packSnorm4x8 unpackUnorm2x16 unpackSnorm2x16 unpackUnorm4x8 unpackSnorm4x8 packDouble2x32 unpackDouble2x32 packHalf2x16 unpackHalf2x16 length distance dot cross normalize ftransform faceforward reflect refract matrixCompMult outerProduct transpose determinant inverse lessThan lessThanEqual greaterThan greaterThanEqual equal notEqual any all not uaddCarry usubBorrow umulExtended imulExtended bitfieldExtract bitfieldInsert bitfieldReverse bitCount findLSB findMSB textureSize textureQueryLod textureQueryLevels textureSamples texture textureProj textureLod textureOffset texelFetch texelFetchOffset textureProjOffset textureLodOffset textureProjLod textureProjLodOffset textureGrad textureGradOffset textureProjGrad textureProjGradOffset textureGather textureGatherOffset textureGatherOffsets texture1D texture1DProj texture1DLod texture1DProjLod texture2D texture2DProj texture2DLod texture2DProjLod texture3D texture3DProj texture3DLod texture3DProjLod textureCube textureCubeLod shadow1D shadow2D shadow1DProj shadow2DProj shadow1DLod shadow2DLod shadow1DProjLod shadow2DProjLod atomicCounterIncrement atomicCounterDecrement atomicCounter atomicAdd atomicMin atomicMax atomicAnd atomicOr atomicXor atomicExchange atomicCompSwap atomicLoad atomicStore imageSize imageSamples imageLoad imageStore imageAtomicAdd imageAtomicMin imageAtomicMax imageAtomicAnd imageAtomicOr imageAtomicXor imageAtomicExchange imageAtomicCompSwap imageAtomicLoad imageAtomicStore dFdx dFdy dFdxFine dFdyFine dFdxCoarse dFdyCoarse fwidth fwidthFine fwidthCoarse interpolateAtCentroid interpolateAtSample interpolateAtOffset noise1 noise2 noise3 noise4 EmitStreamVertex EndStreamPrimitive EmitVertex EndPrimitive barrier controlBarrier memoryBarrier memoryBarrierAtomicCounter memoryBarrierBuffer memoryBarrierShared memoryBarrierImage groupMemoryBarrier subpassLoad ballotARB readInvocationARB readInvocationARB readInvocationARB readFirstInvocationARB readFirstInvocationARB readFirstInvocationARB atomicCounterAddARB atomicCounterSubtractARB atomicCounterMinARB atomicCounterMaxARB atomicCounterAndARB atomicCounterOrARB atomicCounterXorARB atomicCounterExchangeARB atomicCounterCompSwapARB anyInvocationARB allInvocationsARB allInvocationsEqualARB traceNV reportIntersectionNV ignoreIntersectionNV terminateRayNV executeCallableNV traceRayEXT reportIntersectionEXT executeCallableEXT rayQueryInitializeEXT rayQueryProceedEXT rayQueryTerminateEXT rayQueryGenerateIntersectionEXT rayQueryConfirmIntersectionEXT rayQueryGetIntersectionTypeEXT rayQueryGetRayTMinEXT rayQueryGetRayFlagsEXT rayQueryGetWorldRayOriginEXT rayQueryGetWorldRayDirectionEXT rayQueryGetIntersectionTEXT rayQueryGetIntersectionInstanceCustomIndexEXT rayQueryGetIntersectionInstanceIdEXT rayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetEXT rayQueryGetIntersectionGeometryIndexEXT rayQueryGetIntersectionPrimitiveIndexEXT rayQueryGetIntersectionBarycentricsEXT rayQueryGetIntersectionFrontFaceEXT rayQueryGetIntersectionCandidateAABBOpaqueEXT rayQueryGetIntersectionObjectRayDirectionEXT rayQueryGetIntersectionObjectRayOriginEXT rayQueryGetIntersectionObjectToWorldEXT rayQueryGetIntersectionWorldToObjectEXT subgroupBarrier subgroupMemoryBarrier subgroupMemoryBarrierBuffer subgroupMemoryBarrierShared subgroupMemoryBarrierImage subgroupElect subgroupAll subgroupAny subgroupAllEqual subgroupBroadcast subgroupBroadcastFirst subgroupBallot subgroupInverseBallot subgroupBallotBitExtract subgroupBallotBitCount subgroupBallotInclusiveBitCount subgroupBallotExclusiveBitCount subgroupBallotFindLSB subgroupBallotFindMSB subgroupShuffle subgroupShuffleXor subgroupShuffleUp subgroupShuffleDown subgroupAdd subgroupMul subgroupMin subgroupMax subgroupAnd subgroupOr subgroupXor subgroupInclusiveAdd subgroupInclusiveMul subgroupInclusiveMin subgroupInclusiveMax subgroupInclusiveAnd subgroupInclusiveOr subgroupInclusiveXor subgroupExclusiveAdd subgroupExclusiveMul subgroupExclusiveMin subgroupExclusiveMax subgroupExclusiveAnd subgroupExclusiveOr subgroupExclusiveXor subgroupClusteredAdd subgroupClusteredMul subgroupClusteredMin subgroupClusteredMax subgroupClusteredAnd subgroupClusteredOr subgroupClusteredXor subgroupQuadBroadcast subgroupQuadSwapHorizontal subgroupQuadSwapVertical subgroupQuadSwapDiagonal".Trim().Split();
			var variables = "gl_NumWorkGroups gl_WorkGroupSize gl_WorkGroupID gl_LocalInvocationID gl_GlobalInvocationID gl_LocalInvocationIndex gl_LocalGroupSizeARB gl_VertexID gl_VertexIndex gl_InstanceID gl_InstanceIndex gl_PerVertex gl_Position gl_PointSize gl_ClipDistance gl_CullDistance gl_in gl_out gl_PrimitiveIDIn gl_InvocationID gl_PrimitiveID gl_Layer gl_ViewportIndex gl_PatchVerticesIn gl_PrimitiveID gl_InvocationID gl_TessLevelOuter gl_TessLevelInner gl_PatchVerticesIn gl_PrimitiveID gl_TessCoord gl_MaxPatchVertices gl_FragColor gl_FragCoord gl_FrontFacing gl_ClipDistance gl_CullDistance gl_PointCoord gl_PrimitiveID gl_SampleID gl_SamplePosition gl_SampleMaskIn gl_Layer gl_ViewportIndex gl_HelperInvocation gl_FragDepth gl_SampleMask gl_DrawIDARB gl_BaseVertexARB gl_BaseInstanceARB gl_SubGroupSizeARB gl_SubGroupInvocationARB gl_SubGroupEqMaskARB gl_SubGroupGeMaskARB gl_SubGroupGtMaskARB gl_SubGroupLeMaskARB gl_SubGroupLtMaskARB gl_LaunchIDNV gl_LaunchSizeNV gl_InstanceCustomIndexNV gl_WorldRayOriginNV gl_WorldRayDirectionNV gl_ObjectRayOriginNV gl_ObjectRayDirectionNV gl_RayTminNV gl_RayTmaxNV gl_IncomingRayFlagsNV gl_HitTNV gl_HitKindNV gl_ObjectToWorldNV gl_WorldToObjectNV gl_RayFlagsNoneNV gl_RayFlagsOpaqueNV gl_RayFlagsNoOpaqueNV gl_RayFlagsTerminateOnFirstHitNV gl_RayFlagsSkipClosestHitShaderNV gl_RayFlagsCullBackFacingTrianglesNV gl_RayFlagsCullFrontFacingTrianglesNV gl_RayFlagsCullOpaqueNV gl_RayFlagsCullNoOpaqueNV gl_RayFlagsNoneEXT gl_RayFlagsOpaqueEXT gl_RayFlagsNoOpaqueEXT gl_RayFlagsTerminateOnFirstHitEXT gl_RayFlagsSkipClosestHitShaderEXT gl_RayFlagsCullBackFacingTrianglesEXT gl_RayFlagsCullFrontFacingTrianglesEXT gl_RayFlagsCullOpaqueEXT gl_RayFlagsCullNoOpaqueEXT gl_RayQueryCommittedIntersectionEXT gl_RayQueryCandidateIntersectionEXT gl_RayQueryCommittedIntersectionNoneEXT gl_RayQueryCommittedIntersectionTriangleEXT gl_RayQueryCommittedIntersectionGeneratedEXT gl_RayQueryCandidateIntersectionTriangleEXT gl_RayQueryCandidateIntersectionAABBEXT gl_PrimitiveID gl_InstanceID gl_LaunchIDEXT gl_LaunchSizeEXT gl_InstanceCustomIndexEXT gl_GeometryIndexEXT gl_WorldRayOriginEXT gl_WorldRayDirectionEXT gl_ObjectRayOriginEXT gl_ObjectRayDirectionEXT gl_RayTminEXT gl_RayTmaxEXT gl_IncomingRayFlagsEXT gl_HitTEXT gl_HitKindEXT gl_ObjectToWorldEXT gl_WorldToObjectEXT gl_WorldToObject3x4EXT gl_ObjectToWorld3x4EXT gl_RayFlagsNoneEXT gl_RayFlagsOpaqueEXT gl_RayFlagsNoOpaqueEXT gl_RayFlagsTerminateOnFirstHitEXT gl_RayFlagsSkipClosestHitShaderEXT gl_RayFlagsCullBackFacingTrianglesEXT gl_RayFlagsCullFrontFacingTrianglesEXT gl_RayFlagsCullOpaqueEXT gl_RayFlagsCullNoOpaqueEXT gl_HitKindFrontFacingTriangleEXT gl_HitKindBackFacingTriangleEXT gl_NumSubgroups gl_SubgroupID gl_SubgroupSize gl_SubgroupInvocationID gl_SubgroupEqMask gl_SubgroupGeMask gl_SubgroupGtMask gl_SubgroupLeMask gl_SubgroupLtMask gl_WarpIDNV gl_SMIDNV gl_RayTmaxEXT gl_ScopeDevice gl_ScopeWorkgroup gl_ScopeSubgroup gl_ScopeInvocation gl_ScopeQueueFamily gl_SemanticsRelaxed gl_SemanticsAcquire gl_SemanticsRelease gl_SemanticsAcquireRelease gl_SemanticsMakeAvailable gl_SemanticsMakeVisible gl_SemanticsVolatile gl_StorageSemanticsNone gl_StorageSemanticsBuffer gl_StorageSemanticsShared gl_StorageSemanticsImage gl_StorageSemanticsOutput".Trim().Split();
			var types = "float double int void bool mat2 mat3 mat4 dmat2 dmat3 dmat4 mat2x2 mat2x3 mat2x4 dmat2x2 dmat2x3 dmat2x4 mat3x2 mat3x3 mat3x4 dmat3x2 dmat3x3 dmat3x4 mat4x2 mat4x3 mat4x4 dmat4x2 dmat4x3 dmat4x4 vec2 vec3 vec4 ivec2 ivec3 ivec4 bvec2 bvec3 bvec4 dvec2 dvec3 dvec4 uint uvec2 uvec3 uvec4 int64_t i64vec2 i64vec3 i64vec4 uint64_t u64vec2 u64vec3 u64vec4 sampler1D sampler2D sampler3D samplerCube sampler1DShadow sampler2DShadow samplerCubeShadow sampler1DArray sampler2DArray sampler1DArrayShadow sampler2DArrayShadow isampler1D isampler2D isampler3D isamplerCube isampler1DArray isampler2DArray usampler1D usampler2D usampler3D usamplerCube usampler1DArray usampler2DArray sampler2DRect sampler2DRectShadow isampler2DRect usampler2DRect samplerBuffer isamplerBuffer usamplerBuffer sampler2DMS isampler2DMS usampler2DMS sampler2DMSArray isampler2DMSArray usampler2DMSArray samplerCubeArray samplerCubeArrayShadow isamplerCubeArray usamplerCubeArray image1D iimage1D uimage1D image2D iimage2D uimage2D image3D iimage3D uimage3D image2DRect iimage2DRect uimage2DRect imageCube iimageCube uimageCube imageBuffer iimageBuffer uimageBuffer image1DArray iimage1DArray uimage1DArray image2DArray iimage2DArray uimage2DArray imageCubeArray iimageCubeArray uimageCubeArray image2DMS iimage2DMS uimage2DMS image2DMSArray iimage2DMSArray uimage2DMSArray accelerationStructureNV rayPayloadNV rayPayloadInNV hitAttributeNV callableDataNV callableDataInNV rayQueryEXT accelerationStructureEXT rayPayloadEXT rayPayloadInEXT hitAttributeEXT callableDataEXT callableDataInEXT shaderRecordEXT traceRayEXT reportIntersectionEXT ignoreIntersectionEXT terminateRayEXT executeCallableEXT shadercallcoherent accelerationStructureEXT float64_t f64vec2 f64vec3 f64vec4 f64mat2 f64mat3 f64mat4 f64mat2x2 f64mat2x3 f64mat2x4 f64mat3x2 f64mat3x3 f64mat3x4 f64mat4x2 f64mat4x3 f64mat4x4 float32_t f32vec2 f32vec3 f32vec4 f32mat2 f32mat3 f32mat4 f32mat2x2 f32mat2x3 f32mat2x4 f32mat3x2 f32mat3x3 f32mat3x4 f32mat4x2 f32mat4x3 f32mat4x4 float16_t f16vec2 f16vec3 f16vec4 f16mat2 f16mat3 f16mat4 f16mat2x2 f16mat2x3 f16mat2x4 f16mat3x2 f16mat3x3 f16mat3x4 f16mat4x2 f16mat4x3 f16mat4x4 int64_t i64vec2 i64vec3 i64vec4 uint64_t u64vec2 u64vec3 u64vec4 int32_t i32vec2 i32vec3 i32vec4 uint32_t u32vec2 u32vec3 u32vec4 int16_t i16vec2 i16vec3 i16vec4 uint16_t u16vec2 u16vec3 u16vec4 int8_t i8vec2 i8vec3 i8vec4 uint8_t u8vec2 u8vec3 u8vec4".Trim().Split();

			var result = new Dictionary<string, SfxTokenType>();

			result.AddRange(keywords, SfxTokenType.Keyword);
			result.AddRange(functions, SfxTokenType.Function);
			result.AddRange(variables, SfxTokenType.Variable);
			result.AddRange(types, SfxTokenType.Type);
			return result;

		}

		private static HashSet<string> GetCommands()
		{
			var commands = "import material pass vertex fragment mesh closesthit intersection anyhit geometry hull domain option cbuffer buffer draw".Trim().Split();
			var types = "float double int void bool mat2 mat3 mat4 dmat2 dmat3 dmat4 mat2x2 mat2x3 mat2x4 dmat2x2 dmat2x3 dmat2x4 mat3x2 mat3x3 mat3x4 dmat3x2 dmat3x3 dmat3x4 mat4x2 mat4x3 mat4x4 dmat4x2 dmat4x3 dmat4x4 vec2 vec3 vec4 ivec2 ivec3 ivec4 bvec2 bvec3 bvec4 dvec2 dvec3 dvec4 uint uvec2 uvec3 uvec4 int64_t i64vec2 i64vec3 i64vec4 uint64_t u64vec2 u64vec3 u64vec4 sampler1D sampler2D sampler3D samplerCube sampler1DShadow sampler2DShadow samplerCubeShadow sampler1DArray sampler2DArray sampler1DArrayShadow sampler2DArrayShadow isampler1D isampler2D isampler3D isamplerCube isampler1DArray isampler2DArray usampler1D usampler2D usampler3D usamplerCube usampler1DArray usampler2DArray sampler2DRect sampler2DRectShadow isampler2DRect usampler2DRect samplerBuffer isamplerBuffer usamplerBuffer sampler2DMS isampler2DMS usampler2DMS sampler2DMSArray isampler2DMSArray usampler2DMSArray samplerCubeArray samplerCubeArrayShadow isamplerCubeArray usamplerCubeArray image1D iimage1D uimage1D image2D iimage2D uimage2D image3D iimage3D uimage3D image2DRect iimage2DRect uimage2DRect imageCube iimageCube uimageCube imageBuffer iimageBuffer uimageBuffer image1DArray iimage1DArray uimage1DArray image2DArray iimage2DArray uimage2DArray imageCubeArray iimageCubeArray uimageCubeArray image2DMS iimage2DMS uimage2DMS image2DMSArray iimage2DMSArray uimage2DMSArray accelerationStructureNV rayPayloadNV rayPayloadInNV hitAttributeNV callableDataNV callableDataInNV rayQueryEXT accelerationStructureEXT rayPayloadEXT rayPayloadInEXT hitAttributeEXT callableDataEXT callableDataInEXT shaderRecordEXT traceRayEXT reportIntersectionEXT ignoreIntersectionEXT terminateRayEXT executeCallableEXT shadercallcoherent accelerationStructureEXT float64_t f64vec2 f64vec3 f64vec4 f64mat2 f64mat3 f64mat4 f64mat2x2 f64mat2x3 f64mat2x4 f64mat3x2 f64mat3x3 f64mat3x4 f64mat4x2 f64mat4x3 f64mat4x4 float32_t f32vec2 f32vec3 f32vec4 f32mat2 f32mat3 f32mat4 f32mat2x2 f32mat2x3 f32mat2x4 f32mat3x2 f32mat3x3 f32mat3x4 f32mat4x2 f32mat4x3 f32mat4x4 float16_t f16vec2 f16vec3 f16vec4 f16mat2 f16mat3 f16mat4 f16mat2x2 f16mat2x3 f16mat2x4 f16mat3x2 f16mat3x3 f16mat3x4 f16mat4x2 f16mat4x3 f16mat4x4 int64_t i64vec2 i64vec3 i64vec4 uint64_t u64vec2 u64vec3 u64vec4 int32_t i32vec2 i32vec3 i32vec4 uint32_t u32vec2 u32vec3 u32vec4 int16_t i16vec2 i16vec3 i16vec4 uint16_t u16vec2 u16vec3 u16vec4 int8_t i8vec2 i8vec3 i8vec4 uint8_t u8vec2 u8vec3 u8vec4".Trim().Split();
			var result = new HashSet<string>(commands);
			result.UnionWith(types);
			return result;
		}
				
		private static HashSet<string> GetCommandParams()
		{
			var commandParams = "default values type enableIf dim layout stages attribs binding".Trim().Split();
			return new HashSet<string>(commandParams);
		}
	}
}
