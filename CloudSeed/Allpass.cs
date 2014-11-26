﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSeed
{
	public class Allpass
	{
		private readonly double[] buffer;
		private readonly double[] output;
		private readonly int len;
		private int index;

		public int SampleDelay;
		public double Feedback;
		
		public Allpass(int bufferSize, int sampleDelay)
		{
			this.len = bufferSize;
			this.buffer = new double[bufferSize];
			this.output = new double[bufferSize];
			this.SampleDelay = sampleDelay;
			index = bufferSize - 1;
		}

		public double[] Output { get { return output; } }
		
		public void Process(double[] input, int sampleCount)
		{
			int indexread = (index + SampleDelay) % len;

			for (int i = 0; i < sampleCount; i++)
			{
				if (index < 0) index += len;
				if (indexread < 0) indexread += len;

				var bufOut = buffer[indexread];
				var inVal = input[i] + bufOut * Feedback;
				buffer[index] = inVal;
				output[i] = bufOut - inVal * Feedback;
				index--;
				indexread--;
			}
		}
	}
}
