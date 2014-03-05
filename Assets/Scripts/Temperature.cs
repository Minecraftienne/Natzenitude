using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class Temperature {

	//Volume en dm 3 = litre
	public double volume;

	//double temperatureExterieur;
    //double temperatureInterieur;

	int puissance;
	public int joule;
	public int temps;
	public double temperature;
	string temperatureText;

	//volume passer en cm3
	public void init(double volume, int puissance) {

		// this.temperatureExterieur = 20;
        // this.temperatureInterieur = 20;

		this.volume = volume;
		this.puissance = puissance;
		joule = (int)puissance / 10;
		temps = 10;
		this.volume = (this.volume / 1000); //conversion en dm 3
		temperature = 20;
	}
	
	public void up(int puissance) {

		this.puissance = puissance;
		this.joule = (int)this.puissance / 10;
	}
	
	public void calcul() {

		// Debug.Log((this.puissance / 20));
		if (temperature < (this.puissance / 20)) {

			Debug.Log("température : " + temperature);
			this.temperature = this.temperature + (this.joule / this.volume);
		}
	}
}