var speed:float=8000;
//var root:GameObject;
private var rnd:int;
private var timer:float;
var myRoot:GameObject;
var myBodyZone:GameObject;
var myDangerZone:GameObject;
var HP:float=3;
var damage:float=1;

//var bloodParticle:ParticleSystem;
private var state:int=1;  //1 run, 2 fight, 3 dead




function Start () {



Destroy(myRoot, 40);
}




function FixedUpdate () 
{

if(HP<0) state=3;

if(state==3) //died
	{
		timer+=Time.deltaTime;
		myRoot.collider.enabled=false;
		Destroy(myRoot.rigidbody);

//		bloodParticle.emissionRate=0;
		
		
		if (!myRoot.animation.IsPlaying("die1")&&!myRoot.animation.IsPlaying("die2"))
					{
rnd=Random.Range(1, 3);
if (rnd==1)	myRoot.animation.CrossFade("die1");
if (rnd==2) myRoot.animation.CrossFade("die2");
					}
		
		
		if (timer>2) myRoot.transform.Translate(0, -0.2*Time.deltaTime, 0);
		
		if (timer>10) Destroy(myRoot);
	}



if(state==1) //running
	{
	//bloodParticle.emissionRate=0;
myRoot.rigidbody.AddRelativeForce (0, 0 ,speed*Time.deltaTime*(7-myRoot.rigidbody.velocity.magnitude));
myRoot.rigidbody.drag=0.5;

		if (!myRoot.animation.IsPlaying("run"))
			{
myRoot.animation.CrossFade("run");

			}
	}





if(state==2)  //attacking
	{
	//bloodParticle.emissionRate=5;
state=1;
	
	
	if (!myRoot.animation.IsPlaying("attack1")&&!myRoot.animation.IsPlaying("attack2"))
		{
		rnd=Random.Range(1, 3);
		if (rnd==1) myRoot.animation.CrossFade("attack1");
		if (rnd==2) myRoot.animation.CrossFade("attack2");
		}
	}


	
}


function OnTriggerStay (other : Collider) {


  if (other.tag=="bodyZone"&&state!=3)
	{
other.transform.Find("controller").GetComponent(AC_CreaturesBehaviour).HP-=damage*Time.deltaTime;
myRoot.rigidbody.drag=10;

	state=2;	
	}

}
