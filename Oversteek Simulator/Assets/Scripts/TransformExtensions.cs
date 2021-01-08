using UnityEngine;

namespace Assets.Scripts
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Get a transform from the any of the children based on the tag.
        /// </summary>
        public static Transform GetChildrenByTag(this Transform transform, string tag)
        {
            // Check if there are any children at all.
            if (transform.childCount <= 0) return null;

            // Loop through all children.
            foreach (Transform x in transform)
            {
                // Compare the tag. If it's the right tag, return the child.
                if (x.CompareTag(tag)) return x;

                // Check if the child's children have the tag.
                var find = x.GetChildrenByTag(tag);
                if (find != null) return find;
            }
            return null;
        }
    }
}
