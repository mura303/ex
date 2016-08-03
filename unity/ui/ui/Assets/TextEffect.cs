using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TextEffect : UIBehaviour, IMeshModifier {

	public new  void  OnValidate()
	{
		base.OnValidate ();

		var graphics = base.GetComponent<Graphic> ();
		if (graphics != null) {
			graphics.SetVerticesDirty ();
		}
	}

	public void ModifyMesh (Mesh mesh){}
	public void ModifyMesh (VertexHelper verts)
	{
		var stream = ListPool<UIVertex>.Get ();
		verts.GetUIVertexStream (stream);

		modify (ref stream);

		verts.Clear();
		verts.AddUIVertexTriangleStream(stream);

		ListPool<UIVertex>.Release (stream);
	}

	void modify( ref List<UIVertex> stream ){
		float angle = 70;
		// 6文字ずつ進む
		for (int i = 0, streamCount = stream.Count; i < streamCount; i+= 6) {
			// 文字の中央を取得（上なら[i+1]）
			var center = Vector2.Lerp(stream [i].position, stream [i + 3].position , 0.5f) ;
			// 頂点を回す
			for (int r = 0; r < 6; r++) {
				var element = stream [i+r];

				var pos = element.position - (Vector3)center;;
				var newPos = new Vector2(
					pos.x * Mathf.Cos (angle * Mathf.Deg2Rad) - pos.y * Mathf.Sin (angle  * Mathf.Deg2Rad),
					pos.x * Mathf.Sin (angle * Mathf.Deg2Rad) + pos.y * Mathf.Cos (angle  * Mathf.Deg2Rad));
				element.position = (Vector3)(newPos + center);

				stream [i+r] = element;
			}
		}
	}
}
