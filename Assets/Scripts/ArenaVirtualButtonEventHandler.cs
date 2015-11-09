using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

public class ArenaVirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler {

    [SerializeField] GameObject floor;
    [SerializeField] GameObject palmTrees;
    [SerializeField] GameObject cacti;
    [SerializeField] GameObject rocks;
    [SerializeField] GameObject deadTrees;
    [SerializeField] GameObject boundaries;

    private class ArenaColor
    {
        public Color floor;
        public Color leafs;
        public Color rock;
        public Color wood;

        public ArenaColor(Color floor, Color leafs, Color rock, Color wood)
        {
            this.floor = floor;
            this.leafs = leafs;
            this.rock = rock;
            this.wood = wood;
        }
    }

    private List<ArenaColor> arenaColors;
    private int index;

	// Use this for initialization
	void Start ()
    {
        arenaColors = new List<ArenaColor>();

        //Default
        Color defaultFloor = Color.white;
        Color defaultLeafs = new Color(111 / 255f, 118 / 255f, 103 / 255f);
        Color defaultRock = new Color(156 / 255f, 138 / 255f, 116 / 255f);
        Color defaultWood = new Color(81 / 255f, 52 / 255f, 31 / 255f);
        ArenaColor defaultColor = new ArenaColor(defaultFloor, defaultLeafs, defaultRock, defaultWood);
        
        //Dark
        Color darkFloor = new Color(78 / 255f, 84 / 255f, 135 / 255f);
        Color darkLeafs = new Color(219 / 255f, 119 / 255f, 102 / 255f);
        Color darkRock = new Color(49 / 255f, 49 / 255f, 49 / 255f);
        Color darkWood = new Color(227 / 255f, 227 / 255f, 227 / 255f);
        ArenaColor darkColor = new ArenaColor(darkFloor, darkLeafs, darkRock, darkWood);

        //Jungle
        Color jungleFloor = new Color(169 / 255f, 136 / 255f, 84 / 255f);
        Color jungleLeafs = new Color(0 / 255f, 227 / 255f, 0 / 255f);
        Color jungleRock = new Color(81 / 255f, 58 / 255f, 34 / 255f);
        Color jungleWood = new Color(150 / 255f, 115 / 255f, 0 / 255f);
        ArenaColor jungleColor = new ArenaColor(jungleFloor, jungleLeafs, jungleRock, jungleWood);

        arenaColors.Add(darkColor);
        arenaColors.Add(jungleColor);
        arenaColors.Add(defaultColor);

        index = 0;

        // Search for all Children from this ImageTarget with type VirtualButtonBehaviour
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            // Register with the virtual buttons TrackableBehaviour
            vbs[i].RegisterEventHandler(this);
        }
	}

    void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        ArenaColor nextArena = arenaColors[index];
        index = (index + 1) >= arenaColors.Count ? 0 : index + 1;

        //floor
        floor.gameObject.GetComponent<MeshRenderer>().material.color = nextArena.floor;

        //palm trees
        foreach(Transform palmTree in palmTrees.transform)
        {
            MeshRenderer renderer = palmTree.gameObject.GetComponent<MeshRenderer>();
            renderer.materials[0].color = nextArena.wood;
            renderer.materials[1].color = nextArena.leafs;
        }

        //cacti
        foreach(Transform cactus in cacti.transform)
        {
            cactus.gameObject.GetComponent<MeshRenderer>().material.color = nextArena.leafs;
        }

        //rocks
        foreach(Transform rock in rocks.transform)
        {
            rock.gameObject.GetComponent<MeshRenderer>().material.color = nextArena.rock;
        }

        //dead trees
        foreach(Transform deadTree in deadTrees.transform)
        {
            deadTree.gameObject.GetComponent<MeshRenderer>().material.color = nextArena.wood;
        }

        //boundaries
        foreach(Transform boundary in boundaries.transform)
        {
            boundary.gameObject.GetComponent<MeshRenderer>().material.color = nextArena.rock;
        }
    }

    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
    }
}
