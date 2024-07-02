using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"DOTween.dll",
		"Luban.Runtime.dll",
		"Main.dll",
		"Nino.Serialization.dll",
		"Nino.Shared.dll",
		"System.Core.dll",
		"System.Runtime.CompilerServices.Unsafe.dll",
		"System.dll",
		"UniTask.dll",
		"UnityEngine.CoreModule.dll",
		"YooAsset.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.BattleRoomService.<Awake>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.BattleUnitService.<Awake>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__0>d,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__1>d,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.Cfg.ConfigService.<Awake>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.Cfg.ConfigService.<LoadAll>d__11>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.Cfg.ConfigService.<Loader>d__2,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.GameEntry.<StartServices>d__10>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.GameServiceBehaviour.<StartServices>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.GameServiceManager.<StartServices>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.LoginService.<Login>d__12>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.PlayerDataService.<Awake>d__11>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.SceneService.<Awake>d__0>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.SceneService.<ChangeScene>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.SceneService.<ChangeToBattleScene>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.SceneService.<ChangeToHomeScene>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.SceneService.<ChangeToLoginScene>d__2>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.UI3D_DamageText.<StartFloating>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<Game.UnitModelComponent.<PlayAnimationAndWaitAsync>d__7>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.BattleRoomService.<Awake>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.BattleUnitService.<Awake>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__0>d,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__1>d,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.Cfg.ConfigService.<Awake>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.Cfg.ConfigService.<LoadAll>d__11>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.Cfg.ConfigService.<Loader>d__2,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.GameEntry.<StartServices>d__10>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.GameServiceBehaviour.<StartServices>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.GameServiceManager.<StartServices>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.LoginService.<Login>d__12>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.PlayerDataService.<Awake>d__11>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.SceneService.<Awake>d__0>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.SceneService.<ChangeScene>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.SceneService.<ChangeToBattleScene>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.SceneService.<ChangeToHomeScene>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.SceneService.<ChangeToLoginScene>d__2>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.UI3D_DamageText.<StartFloating>d__1>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<Game.UnitModelComponent.<PlayAnimationAndWaitAsync>d__7>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>
	// Cysharp.Threading.Tasks.CompilerServices.IStateMachineRunnerPromise<object>
	// Cysharp.Threading.Tasks.ITaskPoolNode<object>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.IUniTaskSource<object>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<object>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<object>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<object>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask<object>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<Cysharp.Threading.Tasks.AsyncUnit>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<object>
	// Nino.Serialization.NinoWrapperBase<object>
	// Nino.Shared.IO.ExtensibleBuffer<byte>
	// Nino.Shared.UncheckedStack.Enumerator<object>
	// Nino.Shared.UncheckedStack<object>
	// System.Action<Nino.Serialization.TypeModel.NinoMember>
	// System.Action<byte>
	// System.Action<object>
	// System.ByReference<byte>
	// System.Collections.Concurrent.ConcurrentDictionary.<GetEnumerator>d__35<object,byte>
	// System.Collections.Concurrent.ConcurrentDictionary.<GetEnumerator>d__35<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.DictionaryEnumerator<object,byte>
	// System.Collections.Concurrent.ConcurrentDictionary.DictionaryEnumerator<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.Node<object,byte>
	// System.Collections.Concurrent.ConcurrentDictionary.Node<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary.Tables<object,byte>
	// System.Collections.Concurrent.ConcurrentDictionary.Tables<object,object>
	// System.Collections.Concurrent.ConcurrentDictionary<object,byte>
	// System.Collections.Concurrent.ConcurrentDictionary<object,object>
	// System.Collections.Generic.ArraySortHelper<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.ArraySortHelper<byte>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.Comparer<byte>
	// System.Collections.Generic.Comparer<float>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Comparer<uint>
	// System.Collections.Generic.Dictionary.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection<int,long>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection<int,long>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<int,int>
	// System.Collections.Generic.Dictionary<int,long>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.EqualityComparer<byte>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<long>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.ICollection<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<byte>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.IComparer<byte>
	// System.Collections.Generic.IComparer<float>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IComparer<uint>
	// System.Collections.Generic.IDictionary<object,byte>
	// System.Collections.Generic.IDictionary<object,object>
	// System.Collections.Generic.IEnumerable<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.IEnumerable<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,byte>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<byte>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<Cysharp.Threading.Tasks.UniTask>
	// System.Collections.Generic.IEnumerator<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,byte>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<byte>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.IList<byte>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<int,int>
	// System.Collections.Generic.KeyValuePair<int,long>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<object,byte>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.KeyValuePair<uint,object>
	// System.Collections.Generic.List.Enumerator<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.List.Enumerator<byte>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.List<byte>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.ObjectComparer<byte>
	// System.Collections.Generic.ObjectComparer<float>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectComparer<uint>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.ObjectEqualityComparer<byte>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<long>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<uint,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<uint,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<uint,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<uint,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<uint,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<uint,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<uint,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<uint,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<uint,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<uint,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<uint,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<uint,object>
	// System.Collections.Generic.SortedDictionary<uint,object>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<object>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<uint,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<Nino.Serialization.TypeModel.NinoMember>
	// System.Collections.ObjectModel.ReadOnlyCollection<byte>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<Nino.Serialization.TypeModel.NinoMember>
	// System.Comparison<byte>
	// System.Comparison<object>
	// System.Func<Cysharp.Threading.Tasks.UniTask,byte>
	// System.Func<Cysharp.Threading.Tasks.UniTask<object>>
	// System.Func<System.Collections.Generic.KeyValuePair<int,object>,int>
	// System.Func<byte>
	// System.Func<int>
	// System.Func<object,Cysharp.Threading.Tasks.UniTask<object>>
	// System.Func<object,Cysharp.Threading.Tasks.UniTask>
	// System.Func<object,byte>
	// System.Func<object,float>
	// System.Func<object,int>
	// System.Func<object,object>
	// System.Func<object>
	// System.Linq.Buffer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Linq.Buffer<object>
	// System.Linq.Enumerable.Iterator<Cysharp.Threading.Tasks.UniTask>
	// System.Linq.Enumerable.Iterator<object>
	// System.Linq.Enumerable.WhereArrayIterator<object>
	// System.Linq.Enumerable.WhereEnumerableIterator<Cysharp.Threading.Tasks.UniTask>
	// System.Linq.Enumerable.WhereEnumerableIterator<object>
	// System.Linq.Enumerable.WhereListIterator<object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,Cysharp.Threading.Tasks.UniTask>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,Cysharp.Threading.Tasks.UniTask>
	// System.Linq.Enumerable.WhereSelectListIterator<object,Cysharp.Threading.Tasks.UniTask>
	// System.Linq.EnumerableSorter<System.Collections.Generic.KeyValuePair<int,object>,int>
	// System.Linq.EnumerableSorter<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Linq.EnumerableSorter<object,float>
	// System.Linq.EnumerableSorter<object>
	// System.Linq.GroupedEnumerable<object,int,object>
	// System.Linq.IGrouping<int,object>
	// System.Linq.IdentityFunction.<>c<object>
	// System.Linq.IdentityFunction<object>
	// System.Linq.Lookup.<GetEnumerator>d__12<int,object>
	// System.Linq.Lookup.Grouping.<GetEnumerator>d__7<int,object>
	// System.Linq.Lookup.Grouping<int,object>
	// System.Linq.Lookup<int,object>
	// System.Linq.OrderedEnumerable.<GetEnumerator>d__1<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Linq.OrderedEnumerable.<GetEnumerator>d__1<object>
	// System.Linq.OrderedEnumerable<System.Collections.Generic.KeyValuePair<int,object>,int>
	// System.Linq.OrderedEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Linq.OrderedEnumerable<object,float>
	// System.Linq.OrderedEnumerable<object>
	// System.Nullable<float>
	// System.Predicate<Nino.Serialization.TypeModel.NinoMember>
	// System.Predicate<byte>
	// System.Predicate<object>
	// System.ReadOnlySpan<byte>
	// System.Span<byte>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,object>>
	// System.ValueTuple<byte,object>
	// }}

	public void RefMethods()
	{
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.BattleRoomService.<Awake>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.BattleRoomService.<Awake>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.BattleUnitService.<Awake>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.BattleUnitService.<Awake>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.Cfg.ConfigService.<Awake>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.Cfg.ConfigService.<Awake>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.Cfg.ConfigService.<LoadAll>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.Cfg.ConfigService.<LoadAll>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.GameEntry.<StartServices>d__10>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.GameEntry.<StartServices>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.GameServiceBehaviour.<StartServices>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.GameServiceBehaviour.<StartServices>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.GameServiceManager.<StartServices>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.GameServiceManager.<StartServices>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.LoginService.<Login>d__12>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.LoginService.<Login>d__12&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.PlayerDataService.<Awake>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.PlayerDataService.<Awake>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.SceneService.<Awake>d__0>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.SceneService.<Awake>d__0&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.SceneService.<ChangeScene>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.SceneService.<ChangeScene>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.SceneService.<ChangeToBattleScene>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.SceneService.<ChangeToBattleScene>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.SceneService.<ChangeToHomeScene>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.SceneService.<ChangeToHomeScene>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.SceneService.<ChangeToLoginScene>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.SceneService.<ChangeToLoginScene>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.UI3D_DamageText.<StartFloating>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.UI3D_DamageText.<StartFloating>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.UnitModelComponent.<PlayAnimationAndWaitAsync>d__7>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.UnitModelComponent.<PlayAnimationAndWaitAsync>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.Cfg.ConfigService.<Loader>d__2>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.Cfg.ConfigService.<Loader>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__0>d>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__0>d&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__1>d>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__1>d&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.BattleRoomService.<Awake>d__1>(Game.BattleRoomService.<Awake>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.BattleUnitService.<Awake>d__4>(Game.BattleUnitService.<Awake>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.Cfg.ConfigService.<Awake>d__1>(Game.Cfg.ConfigService.<Awake>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.Cfg.ConfigService.<LoadAll>d__11>(Game.Cfg.ConfigService.<LoadAll>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.GameEntry.<StartServices>d__10>(Game.GameEntry.<StartServices>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.GameServiceBehaviour.<StartServices>d__1>(Game.GameServiceBehaviour.<StartServices>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.GameServiceManager.<StartServices>d__3>(Game.GameServiceManager.<StartServices>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.LoginService.<Login>d__12>(Game.LoginService.<Login>d__12&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.PlayerDataService.<Awake>d__11>(Game.PlayerDataService.<Awake>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.SceneService.<Awake>d__0>(Game.SceneService.<Awake>d__0&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.SceneService.<ChangeScene>d__4>(Game.SceneService.<ChangeScene>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.SceneService.<ChangeToBattleScene>d__1>(Game.SceneService.<ChangeToBattleScene>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.SceneService.<ChangeToHomeScene>d__3>(Game.SceneService.<ChangeToHomeScene>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.SceneService.<ChangeToLoginScene>d__2>(Game.SceneService.<ChangeToLoginScene>d__2&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.UI3D_DamageText.<StartFloating>d__1>(Game.UI3D_DamageText.<StartFloating>d__1&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<Game.UnitModelComponent.<PlayAnimationAndWaitAsync>d__7>(Game.UnitModelComponent.<PlayAnimationAndWaitAsync>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__0>d>(Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__0>d&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__1>d>(Game.Cfg.ConfigService.<>c__DisplayClass11_0.<<LoadAll>b__1>d&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<Game.Cfg.ConfigService.<Loader>d__2>(Game.Cfg.ConfigService.<Loader>d__2&)
		// System.Collections.Generic.IEnumerable<Cysharp.Threading.Tasks.UniTask> Cysharp.Threading.Tasks.EnumerableAsyncExtensions.Select<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,Cysharp.Threading.Tasks.UniTask>)
		// Cysharp.Threading.Tasks.UniTask.Awaiter Cysharp.Threading.Tasks.EnumeratorAsyncExtensions.GetAwaiter<object>(object)
		// Cysharp.Threading.Tasks.UniTask<object> Cysharp.Threading.Tasks.UniTask.Create<object>(System.Func<Cysharp.Threading.Tasks.UniTask<object>>)
		// object DG.Tweening.TweenSettingsExtensions.SetEase<object>(object,DG.Tweening.Ease)
		// string Luban.StringUtil.CollectionToString<int,long>(System.Collections.Generic.IDictionary<int,long>)
		// int Nino.Serialization.Reader.Read<int>(int)
		// int Nino.Serialization.Serializer.GetSize<object>(object&,System.Collections.Generic.Dictionary<System.Reflection.MemberInfo,object>)
		// byte[] Nino.Serialization.Serializer.Serialize<object>(object&)
		// int Nino.Serialization.Serializer.Serialize<object>(System.Type,object,System.Collections.Generic.Dictionary<System.Reflection.MemberInfo,object>,System.Span<byte>,int)
		// bool Nino.Serialization.Serializer.TrySerializeArray<object>(object&,Nino.Serialization.Writer&)
		// bool Nino.Serialization.Serializer.TrySerializeCodeGenType<object>(System.Type,object&,Nino.Serialization.Writer&)
		// bool Nino.Serialization.Serializer.TrySerializeDict<object>(object&,Nino.Serialization.Writer&)
		// bool Nino.Serialization.Serializer.TrySerializeEnumType<object>(System.Type,object&,Nino.Serialization.Writer&)
		// bool Nino.Serialization.Serializer.TrySerializeList<object>(object&,Nino.Serialization.Writer&)
		// bool Nino.Serialization.Serializer.TrySerializeUnmanagedType<object>(System.Type,object&,Nino.Serialization.Writer&)
		// bool Nino.Serialization.Serializer.TrySerializeWrapperType<object>(System.Type,object&,Nino.Serialization.Writer&)
		// System.Void Nino.Serialization.Writer.WriteAsUnmanaged<object>(object&,int)
		// System.Void Nino.Serialization.Writer.WriteEnum<object>(object)
		// object System.Activator.CreateInstance<object>()
		// byte[] System.Array.Empty<byte>()
		// int System.Linq.Enumerable.Count<object>(System.Collections.Generic.IEnumerable<object>)
		// object System.Linq.Enumerable.First<object>(System.Collections.Generic.IEnumerable<object>)
		// object System.Linq.Enumerable.FirstOrDefault<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.IEnumerable<System.Linq.IGrouping<int,object>> System.Linq.Enumerable.GroupBy<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Linq.IOrderedEnumerable<object> System.Linq.Enumerable.OrderBy<object,float>(System.Collections.Generic.IEnumerable<object>,System.Func<object,float>)
		// System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<int,object>> System.Linq.Enumerable.OrderByDescending<System.Collections.Generic.KeyValuePair<int,object>,int>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>,System.Func<System.Collections.Generic.KeyValuePair<int,object>,int>)
		// System.Collections.Generic.IEnumerable<Cysharp.Threading.Tasks.UniTask> System.Linq.Enumerable.Select<object,Cysharp.Threading.Tasks.UniTask>(System.Collections.Generic.IEnumerable<object>,System.Func<object,Cysharp.Threading.Tasks.UniTask>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Where<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<Cysharp.Threading.Tasks.UniTask> System.Linq.Enumerable.Iterator<object>.Select<Cysharp.Threading.Tasks.UniTask>(System.Func<object,Cysharp.Threading.Tasks.UniTask>)
		// System.Span<byte> System.MemoryExtensions.AsSpan<byte>(byte[])
		// object System.Reflection.CustomAttributeExtensions.GetCustomAttribute<object>(System.Reflection.MemberInfo)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.BattleRoomService.<OnBattleEnd>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.BattleRoomService.<OnBattleEnd>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.GameEntry.<Awake>d__7>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.GameEntry.<Awake>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.UILoginPanel.<OnClickBtnEnterGame>d__6>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.UILoginPanel.<OnClickBtnEnterGame>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,Game.UnitUI3DComponent.<ShowDamageText>d__8>(Cysharp.Threading.Tasks.UniTask.Awaiter&,Game.UnitUI3DComponent.<ShowDamageText>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,UIHomePanel.<OnClickBtnStartBattle>d__6>(Cysharp.Threading.Tasks.UniTask.Awaiter&,UIHomePanel.<OnClickBtnStartBattle>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<Game.BattleRoomService.<OnBattleEnd>d__4>(Game.BattleRoomService.<OnBattleEnd>d__4&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<Game.GameEntry.<Awake>d__7>(Game.GameEntry.<Awake>d__7&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<Game.UILoginPanel.<OnClickBtnEnterGame>d__6>(Game.UILoginPanel.<OnClickBtnEnterGame>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<Game.UnitUI3DComponent.<ShowDamageText>d__8>(Game.UnitUI3DComponent.<ShowDamageText>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<UIHomePanel.<OnClickBtnStartBattle>d__6>(UIHomePanel.<OnClickBtnStartBattle>d__6&)
		// bool System.Runtime.CompilerServices.RuntimeHelpers.IsReferenceOrContainsReferences<int>()
		// byte& System.Runtime.CompilerServices.Unsafe.Add<byte>(byte&,int)
		// object& System.Runtime.CompilerServices.Unsafe.As<object,object>(object&)
		// System.Void* System.Runtime.CompilerServices.Unsafe.AsPointer<object>(object&)
		// int System.Runtime.CompilerServices.Unsafe.ReadUnaligned<int>(byte&)
		// int System.Runtime.CompilerServices.Unsafe.SizeOf<int>()
		// int System.Runtime.CompilerServices.Unsafe.SizeOf<object>()
		// System.Void System.Runtime.CompilerServices.Unsafe.WriteUnaligned<object>(byte&,object)
		// int System.Runtime.InteropServices.Marshal.SizeOf<object>()
		// byte& System.Runtime.InteropServices.MemoryMarshal.GetReference<byte>(System.ReadOnlySpan<byte>)
		// byte& System.Runtime.InteropServices.MemoryMarshal.GetReference<byte>(System.Span<byte>)
		// int System.Runtime.InteropServices.MemoryMarshal.Read<int>(System.ReadOnlySpan<byte>)
		// System.Void UniFramework.Event.EventGroup.AddListener<object>(System.Action<UniFramework.Event.IEventMessage>)
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>(bool)
		// object[] UnityEngine.Object.FindObjectsOfType<object>()
		// object[] UnityEngine.Resources.ConvertObjects<object>(UnityEngine.Object[])
		// object YooAsset.AssetHandle.GetAssetObject<object>()
		// YooAsset.AssetHandle YooAsset.ResourcePackage.LoadAssetSync<object>(string)
		// YooAsset.AssetHandle YooAsset.YooAssets.LoadAssetSync<object>(string)
	}
}