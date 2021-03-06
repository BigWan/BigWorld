﻿using System;

namespace BW.MathUtil {

    /// <summary>
    /// 振幅叠加的噪声
    /// </summary>
    public class OctavedNoise<T>:INoise where T : INoise {

        /// <summary> 倍频(噪声音阶) </summary>
        private byte octaves;

        /// <summary> 持续性(振幅变化率) </summary>
        private float persistence;

        /// <summary>
        /// 偏移量
        /// </summary>
        private float offset;
        /// <summary>
        /// 缩放
        /// </summary>
        private float scale;

        /// <summary> 基础噪声函数 </summary>
        private T noise;

        public OctavedNoise(T noise,byte octaves,float persistence,float offset = 0,float scale = 1f ) {
            this.noise = noise;
            this.octaves = octaves;
            this.persistence = persistence;
            this.offset = offset;
            this.scale = scale;
        }

        public void SetOS(float offset,float scale) {
            this.offset = offset;
            this.scale = scale;
        }

        public double Noise(double x, double y, double z) {
            double total = 0;
            double maxValue = 0;
            
            int frequency = 1;   // 频率
            double amplitude = 1; // 振幅

            for (int i = 0; i < octaves; i++) {
                total += noise.Noise(x * frequency, y * frequency, z * frequency) * amplitude;
                maxValue += amplitude;
                amplitude *= persistence;
                frequency *= 2;
            }

            return total / maxValue;
        }

        /// <summary>
        /// 带偏移和缩放的噪声
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public double NoiseOS(double x,double y,double z) {
            return Noise(x * scale + offset, y * scale + offset, z * scale + offset);
        }


    }

}