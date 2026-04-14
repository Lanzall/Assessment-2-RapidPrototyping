using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Disble warnings because Unity has a class named Behaviour
#pragma warning disable
	public class BehaviourX : MonoBehaviour
	{
		#region Component Overrides

		Canvas __canvas;

		public Canvas myCanvas
		{
			get
			{
				if (__canvas == null)
					__canvas = gameObject.GetComponentInParent<Canvas>();
				return __canvas;
			}
		}

		CanvasGroup __canvasGroup;

		public CanvasGroup myCanvasGroup
		{
			get
			{
				if (__canvasGroup == null)
					__canvasGroup = gameObject.GetComponent<CanvasGroup>();
				return __canvasGroup;
			}
		}


		RectTransform __rt;

		public RectTransform myRectTransform
		{
			get
			{
				if (__rt == null)
					__rt = gameObject.GetComponent<RectTransform>();
				return __rt;
			}
		}

		Image __image;

		public Image myImage
		{
			get
			{
				if (__image == null)
					__image = gameObject.GetComponent<Image>();
				return __image;
			}
		}

		Button __button;

		public Button myButton
		{
			get
			{
				if (__button == null)
					__button = gameObject.GetComponent<Button>();
				return __button;
			}
		}

		Renderer __renderer;

		public Renderer myRenderer
		{
			get
			{
				if (__renderer == null)
					__renderer = gameObject.GetComponent<Renderer>();
				return __renderer;
			}
		}

		SpriteRenderer __spriteRenderer;

		public SpriteRenderer mySpriteRenderer
		{
			get
			{
				if (__spriteRenderer == null)
					__spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
				return __spriteRenderer;
			}
		}

		Material __material;

		public Material myMaterial
		{
			get
			{
				if (__material == null)
					__material = gameObject.GetComponent<Material>();
				return __material;
			}
		}

		Rigidbody __rigidbody;

		public Rigidbody myRigidbody
		{
			get
			{
				if (__rigidbody == null)
					__rigidbody = gameObject.GetComponent<Rigidbody>();
				return __rigidbody;
			}
		}

		BoxCollider __boxCollider;

		public BoxCollider myBoxCollider
		{
			get
			{
				if (__boxCollider == null)
					__boxCollider = gameObject.GetComponent<BoxCollider>();
				return __boxCollider;
			}
		}

		SphereCollider __spherecollider;

		public SphereCollider mySpherecollider
		{
			get
			{
				if (__spherecollider == null)
					__spherecollider = gameObject.GetComponent<SphereCollider>();
				return __spherecollider;
			}
		}

		Collider __collider;

		public Collider myCollider
		{
			get
			{
				if (__collider == null)
					__collider = gameObject.GetComponent<Collider>();
				return __collider;
			}
		}

		MeshRenderer __mesh;

		public MeshRenderer myMesh
		{
			get
			{
				if (__mesh == null)
					__mesh = gameObject.GetComponent<MeshRenderer>();
				return __mesh;
			}
		}

		Camera __camera;

		public Camera myCamera
		{
			get
			{
				if (__camera == null)
					__camera = gameObject.GetComponent<Camera>();
				return __camera;
			}
		}

		#endregion

		#region Coroutine Helpers

		/// <summary>
		/// Executes the Action block as a Coroutine on the next frame.
		/// </summary>
		/// <param name="func">The Action block</param>
		protected void ExecuteNextFrame(Action func)
		{
			StartCoroutine(ExecuteAfterFramesCoroutine(1, func));
		}

		/// <summary>
		/// Executes the Action block as a Coroutine after X frames.
		/// </summary>
		/// <param name="func">The Action block</param>
		protected void ExecuteAfterFrames(int frames, Action func)
		{
			StartCoroutine(ExecuteAfterFramesCoroutine(frames, func));
		}

		private IEnumerator ExecuteAfterFramesCoroutine(int frames, Action func)
		{
			for (int f = 0; f < frames; f++)
				yield return new WaitForEndOfFrame();
			func();
		}

		/// <summary>
		/// Executes the Action block as a Coroutine after X seconds
		/// </summary>
		/// <param name="seconds">Seconds.</param>
		protected void ExecuteAfterSeconds(float seconds, Action func)
		{
			if (seconds <= 0f)
				func();
			else
				StartCoroutine(ExecuteAfterSecondsCoroutine(seconds, func));
		}

		private IEnumerator ExecuteAfterSecondsCoroutine(float seconds, Action func)
		{
			yield return new WaitForSeconds(seconds);
			func();
		}

		/// <summary>
		/// Executes the Action block as a Coroutine when a condition is met
		/// </summary>
		/// <param name="condition">The Condition block</param>
		/// <param name="func">The Action block</param>
		protected void ExecuteWhenTrue(Func<bool> condition, Action func)
		{
			StartCoroutine(ExecuteWhenTrueCoroutine(condition, func));
		}

		private IEnumerator ExecuteWhenTrueCoroutine(Func<bool> condition, Action func)
		{
			while (condition() == false)
				yield return new WaitForEndOfFrame();
			func();
		}

		public void EnableAfterTime(GameObject _go, float _time)
		{
			ExecuteAfterSeconds(_time, () => { _go.SetActive(true); });
		}

		public void DisableAfterTime(GameObject _go, float _time)
		{
			ExecuteAfterSeconds(_time, () => { _go.SetActive(false); });
		}

		#endregion

		#region Prefabs

		/// <summary>
		/// Applies current changes to this objects prefab (if it is linked to a prefab).
		/// Same as clicking the "Apply" button of a prefab.
		/// Just remember we must always save the scene to actually save prefabs to disk.
		/// </summary>
		public void ApplyToParentPrefab()
		{
#if UNITY_EDITOR
			UnityEngine.Object prefab = PrefabUtility.GetPrefabParent(gameObject);
			if (prefab != null)
				PrefabUtility.ReplacePrefab(gameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);
#endif
		}

		#endregion

		//
		// Misc
		//

		#region Optimization

		protected bool IsNull<T>(T obj)
		{
			return ReferenceEquals(obj, null) || obj.Equals(null);
		}

		protected bool NotNull<T>(T obj)
		{
			return !IsNull(obj);
		}

		#endregion

		/// <summary>
		/// Creates an empty GameObject (container) and add this to it.
		/// In other words, creates a new GameObject level between this and the parent.
		/// </summary>
		/// <returns>The container.</returns>
		/// <param name="name">The container GameObject name</param>
		public GameObject MakeContainer(string name = null)
		{
			GameObject obj = new GameObject();
			obj.transform.SetParent(transform, false);
			obj.transform.position = transform.position;
			obj.name = (!String.IsNullOrEmpty(name) ? name : "container");
			return obj;
		}

		/// <summary>
		/// Creates an empty GameObject (container) and add this to it.
		/// In other words, creates a new GameObject level between this and the parent.
		/// </summary>
		/// <returns>The container.</returns>
		/// <param name="name">The container GameObject name</param>
		public GameObject MakeChildContainer(string name = null, bool first = false)
		{
			GameObject obj = new GameObject();
			if (first)
				obj.transform.SetAsFirstSibling();
			else
				obj.transform.SetAsLastSibling();
			obj.transform.position = transform.position;
			obj.name = (!String.IsNullOrEmpty(name) ? name : "container");
			return obj;
		}
	}