#if UNITY_EDITOR && TEST_FRAMEWORK_INSTALLED
// ReSharper disable NotAccessedField.Local
// ReSharper disable UnusedMember.Local
// ReSharper disable PartialTypeWithSinglePart
using PrimeTween;
using UnityEngine;
using Assert = NUnit.Framework.Assert;
using AssertionException = UnityEngine.Assertions.AssertionException;

[ExecuteInEditMode]
public partial class EditModeTest : MonoBehaviour {
    [SerializeField] TweenSettings _settings = new TweenSettings(1, AnimationCurve.Linear(0, 0, 1, 1));
    Tween tween = test();
    Sequence sequence = Sequence.Create();
    
    static Tween test() {
        Assert.IsTrue(Constants.noInstance, "This test is designed only for Edit mode.");
        PrimeTweenConfig.SetTweensCapacity(10);
        Assert.Throws<AssertionException>(() => PrimeTweenConfig.warnZeroDuration = false);
        Tween.StopAll();
        Tween.GlobalTimeScale(0.5f, 0.1f);
        Tween.GetTweensCount();
        Sequence.Create()
            .ChainCallback(() => {})
            .InsertCallback(0f, delegate {})
            .Group(StartTween())
            .Chain(StartTween())
            .Insert(0f, Sequence.Create())
            .Insert(0, StartTween());
        Tween.Delay(new object(), 1f, () => { });
        Tween.Delay(new object(), 1f, _ => {});
        Tween.Delay(1f, () => { });
        return Tween.Custom(0, 1, 1, delegate {});
    }

    static Tween StartTween() => Tween.Custom(0f, 1f, 1f, delegate { });
    
    void Awake() => test();
    void OnValidate() => test();
    void Reset() => test();
    void OnEnable() => test();
    void OnDisable() => test();
    void OnDestroy() => test();
}

/*[UnityEditor.InitializeOnLoad]
public partial class EditModeTest {
    static EditModeTest() => test();
    EditModeTest() => test();

    [RuntimeInitializeOnLoadMethod]
    static void runtimeInitOnLoad() => test();
}*/
#endif