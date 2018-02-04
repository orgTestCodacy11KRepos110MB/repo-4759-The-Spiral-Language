module SpiralExample.Main
let cuda_kernels = """
#include "cub/cub.cuh"

extern "C" {
    struct Tuple0 {
        float mem_0;
        float mem_1;
    };
    __device__ __forceinline__ Tuple0 make_Tuple0(float mem_0, float mem_1){
        Tuple0 tmp;
        tmp.mem_0 = mem_0;
        tmp.mem_1 = mem_1;
        return tmp;
    }
    struct Tuple2 {
        Tuple0 mem_0;
        Tuple0 mem_1;
    };
    __device__ __forceinline__ Tuple2 make_Tuple2(Tuple0 mem_0, Tuple0 mem_1){
        Tuple2 tmp;
        tmp.mem_0 = mem_0;
        tmp.mem_1 = mem_1;
        return tmp;
    }
    typedef Tuple0(*FunPointer1)(Tuple0, Tuple0);
    __global__ void method_16(long long int var_0, float * var_1, float * var_2, float * var_3);
    __global__ void method_19(float * var_0, long long int var_1, float * var_2);
    __global__ void method_20(float * var_0, float * var_1, long long int var_2, float * var_3, long long int var_4);
    __global__ void method_23(float var_0, float var_1, float * var_2, float * var_3, long long int var_4, float * var_5);
    __global__ void method_24(float * var_0, float * var_1, float * var_2, long long int var_3, float * var_4);
    __global__ void method_26(long long int var_0, float * var_1, float * var_2);
    __global__ void method_28(float * var_0, float * var_1);
    __global__ void method_30(float * var_0, float * var_1);
    __global__ void method_33(float * var_0, float * var_1, float * var_2);
    __global__ void method_35(float * var_0, float * var_1);
    __global__ void method_37(float * var_0, float * var_1, float * var_2);
    __global__ void method_40(float * var_0, float * var_1, float * var_2);
    __device__ char method_17(long long int * var_0);
    __device__ char method_18(long long int var_0, long long int * var_1);
    __device__ char method_21(long long int var_0, long long int * var_1, float * var_2);
    __device__ char method_27(long long int * var_0, float * var_1);
    __device__ char method_29(long long int * var_0);
    __device__ char method_34(long long int * var_0);
    __device__ char method_36(long long int * var_0);
    __device__ char method_38(long long int * var_0, float * var_1);
    __device__ char method_41(long long int * var_0, float * var_1, float * var_2);
    __device__ Tuple0 method_42(Tuple0 var_0, Tuple0 var_1);
    
    __global__ void method_16(long long int var_0, float * var_1, float * var_2, float * var_3) {
        long long int var_4 = blockDim.y;
        long long int var_5 = threadIdx.x;
        long long int var_6 = blockIdx.x;
        long long int var_7 = (10 * var_6);
        long long int var_8 = (var_5 + var_7);
        long long int var_9[1];
        var_9[0] = var_8;
        while (method_17(var_9)) {
            long long int var_11 = var_9[0];
            long long int var_12 = (var_11 + 10);
            char var_13 = (var_11 >= 0);
            char var_15;
            if (var_13) {
                var_15 = (var_11 < 10);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // "Argument out of bounds."
            } else {
            }
            long long int var_17 = threadIdx.y;
            long long int var_18 = blockIdx.y;
            long long int var_19 = (var_4 * var_18);
            long long int var_20 = (var_17 + var_19);
            long long int var_21[1];
            var_21[0] = var_20;
            while (method_18(var_0, var_21)) {
                long long int var_23 = var_21[0];
                long long int var_24 = (var_23 + var_4);
                char var_25 = (var_23 >= 0);
                char var_27;
                if (var_25) {
                    var_27 = (var_23 < var_0);
                } else {
                    var_27 = 0;
                }
                char var_28 = (var_27 == 0);
                if (var_28) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_29 = (var_23 * 10);
                char var_31;
                if (var_13) {
                    var_31 = (var_11 < 10);
                } else {
                    var_31 = 0;
                }
                char var_32 = (var_31 == 0);
                if (var_32) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_33 = (var_29 + var_11);
                char var_35;
                if (var_25) {
                    var_35 = (var_23 < var_0);
                } else {
                    var_35 = 0;
                }
                char var_36 = (var_35 == 0);
                if (var_36) {
                    // "Argument out of bounds."
                } else {
                }
                char var_38;
                if (var_13) {
                    var_38 = (var_11 < 10);
                } else {
                    var_38 = 0;
                }
                char var_39 = (var_38 == 0);
                if (var_39) {
                    // "Argument out of bounds."
                } else {
                }
                float var_40 = var_1[var_11];
                float var_41 = var_2[var_33];
                float var_42 = var_3[var_33];
                float var_43 = (var_40 + var_41);
                var_3[var_33] = var_43;
                var_21[0] = var_24;
            }
            long long int var_44 = var_21[0];
            var_9[0] = var_12;
        }
        long long int var_45 = var_9[0];
    }
    __global__ void method_19(float * var_0, long long int var_1, float * var_2) {
        long long int var_3 = gridDim.x;
        long long int var_4 = threadIdx.x;
        long long int var_5 = blockIdx.x;
        long long int var_6 = (128 * var_5);
        long long int var_7 = (var_4 + var_6);
        long long int var_8 = (var_3 * 128);
        long long int var_9[1];
        var_9[0] = var_7;
        while (method_18(var_1, var_9)) {
            long long int var_11 = var_9[0];
            long long int var_12 = (var_11 + var_8);
            char var_13 = (var_11 >= 0);
            char var_15;
            if (var_13) {
                var_15 = (var_11 < var_1);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // "Argument out of bounds."
            } else {
            }
            char var_18;
            if (var_13) {
                var_18 = (var_11 < var_1);
            } else {
                var_18 = 0;
            }
            char var_19 = (var_18 == 0);
            if (var_19) {
                // "Argument out of bounds."
            } else {
            }
            float var_20 = var_0[var_11];
            float var_21 = var_2[var_11];
            float var_22 = (-var_20);
            float var_23 = exp(var_22);
            float var_24 = (1 + var_23);
            float var_25 = (1 / var_24);
            var_2[var_11] = var_25;
            var_9[0] = var_12;
        }
        long long int var_26 = var_9[0];
    }
    __global__ void method_20(float * var_0, float * var_1, long long int var_2, float * var_3, long long int var_4) {
        long long int var_5 = gridDim.x;
        long long int var_6 = threadIdx.x;
        long long int var_7 = blockIdx.x;
        long long int var_8 = (256 * var_7);
        long long int var_9 = (var_6 + var_8);
        long long int var_10 = (var_5 * 256);
        float var_11 = 0;
        long long int var_12[1];
        float var_13[1];
        var_12[0] = var_9;
        var_13[0] = var_11;
        while (method_21(var_2, var_12, var_13)) {
            long long int var_15 = var_12[0];
            float var_16 = var_13[0];
            long long int var_17 = (var_15 + var_10);
            char var_18 = (var_15 >= 0);
            char var_20;
            if (var_18) {
                var_20 = (var_15 < var_2);
            } else {
                var_20 = 0;
            }
            char var_21 = (var_20 == 0);
            if (var_21) {
                // "Argument out of bounds."
            } else {
            }
            float var_22 = var_0[var_15];
            float var_23 = var_1[var_15];
            float var_24 = log(var_22);
            float var_25 = (var_23 * var_24);
            float var_26 = (1 - var_23);
            float var_27 = (1 - var_22);
            float var_28 = log(var_27);
            float var_29 = (var_26 * var_28);
            float var_30 = (var_25 + var_29);
            float var_31 = (-var_30);
            float var_32 = (var_16 + var_31);
            var_12[0] = var_17;
            var_13[0] = var_32;
        }
        long long int var_33 = var_12[0];
        float var_34 = var_13[0];
        float var_35 = cub::BlockReduce<float,256,cub::BLOCK_REDUCE_WARP_REDUCTIONS,1,1>().Sum(var_34);
        long long int var_36 = threadIdx.x;
        char var_37 = (var_36 == 0);
        if (var_37) {
            long long int var_38 = blockIdx.x;
            char var_39 = (var_38 >= 0);
            char var_41;
            if (var_39) {
                var_41 = (var_38 < var_4);
            } else {
                var_41 = 0;
            }
            char var_42 = (var_41 == 0);
            if (var_42) {
                // "Argument out of bounds."
            } else {
            }
            var_3[var_38] = var_35;
        } else {
        }
    }
    __global__ void method_23(float var_0, float var_1, float * var_2, float * var_3, long long int var_4, float * var_5) {
        long long int var_6 = gridDim.x;
        long long int var_7 = threadIdx.x;
        long long int var_8 = blockIdx.x;
        long long int var_9 = (128 * var_8);
        long long int var_10 = (var_7 + var_9);
        long long int var_11 = (var_6 * 128);
        long long int var_12[1];
        var_12[0] = var_10;
        while (method_18(var_4, var_12)) {
            long long int var_14 = var_12[0];
            long long int var_15 = (var_14 + var_11);
            char var_16 = (var_14 >= 0);
            char var_18;
            if (var_16) {
                var_18 = (var_14 < var_4);
            } else {
                var_18 = 0;
            }
            char var_19 = (var_18 == 0);
            if (var_19) {
                // "Argument out of bounds."
            } else {
            }
            char var_21;
            if (var_16) {
                var_21 = (var_14 < var_4);
            } else {
                var_21 = 0;
            }
            char var_22 = (var_21 == 0);
            if (var_22) {
                // "Argument out of bounds."
            } else {
            }
            float var_23 = var_2[var_14];
            float var_24 = var_3[var_14];
            float var_25 = var_5[var_14];
            float var_26 = (var_23 - var_24);
            float var_27 = (1 - var_23);
            float var_28 = (var_23 * var_27);
            float var_29 = (var_26 / var_28);
            float var_30 = (var_0 * var_29);
            float var_31 = (var_25 + var_30);
            var_5[var_14] = var_31;
            var_12[0] = var_15;
        }
        long long int var_32 = var_12[0];
    }
    __global__ void method_24(float * var_0, float * var_1, float * var_2, long long int var_3, float * var_4) {
        long long int var_5 = gridDim.x;
        long long int var_6 = threadIdx.x;
        long long int var_7 = blockIdx.x;
        long long int var_8 = (128 * var_7);
        long long int var_9 = (var_6 + var_8);
        long long int var_10 = (var_5 * 128);
        long long int var_11[1];
        var_11[0] = var_9;
        while (method_18(var_3, var_11)) {
            long long int var_13 = var_11[0];
            long long int var_14 = (var_13 + var_10);
            char var_15 = (var_13 >= 0);
            char var_17;
            if (var_15) {
                var_17 = (var_13 < var_3);
            } else {
                var_17 = 0;
            }
            char var_18 = (var_17 == 0);
            if (var_18) {
                // "Argument out of bounds."
            } else {
            }
            char var_20;
            if (var_15) {
                var_20 = (var_13 < var_3);
            } else {
                var_20 = 0;
            }
            char var_21 = (var_20 == 0);
            if (var_21) {
                // "Argument out of bounds."
            } else {
            }
            float var_22 = var_0[var_13];
            float var_23 = var_1[var_13];
            float var_24 = var_2[var_13];
            float var_25 = var_4[var_13];
            float var_26 = (1 - var_24);
            float var_27 = (var_24 * var_26);
            float var_28 = (var_23 * var_27);
            float var_29 = (var_25 + var_28);
            var_4[var_13] = var_29;
            var_11[0] = var_14;
        }
        long long int var_30 = var_11[0];
    }
    __global__ void method_26(long long int var_0, float * var_1, float * var_2) {
        long long int var_3 = threadIdx.x;
        long long int var_4 = blockIdx.x;
        long long int var_5 = (10 * var_4);
        long long int var_6 = (var_3 + var_5);
        long long int var_7[1];
        var_7[0] = var_6;
        while (method_17(var_7)) {
            long long int var_9 = var_7[0];
            long long int var_10 = (var_9 + 10);
            char var_11 = (var_9 >= 0);
            char var_13;
            if (var_11) {
                var_13 = (var_9 < 10);
            } else {
                var_13 = 0;
            }
            char var_14 = (var_13 == 0);
            if (var_14) {
                // "Argument out of bounds."
            } else {
            }
            char var_16;
            if (var_11) {
                var_16 = (var_9 < 10);
            } else {
                var_16 = 0;
            }
            char var_17 = (var_16 == 0);
            if (var_17) {
                // "Argument out of bounds."
            } else {
            }
            long long int var_18 = threadIdx.y;
            long long int var_19 = blockIdx.y;
            long long int var_20 = (32 * var_19);
            long long int var_21 = (var_18 + var_20);
            float var_22 = 0;
            long long int var_23[1];
            float var_24[1];
            var_23[0] = var_21;
            var_24[0] = var_22;
            while (method_21(var_0, var_23, var_24)) {
                long long int var_26 = var_23[0];
                float var_27 = var_24[0];
                long long int var_28 = (var_26 + 32);
                char var_29 = (var_26 >= 0);
                char var_31;
                if (var_29) {
                    var_31 = (var_26 < var_0);
                } else {
                    var_31 = 0;
                }
                char var_32 = (var_31 == 0);
                if (var_32) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_33 = (var_26 * 10);
                char var_35;
                if (var_11) {
                    var_35 = (var_9 < 10);
                } else {
                    var_35 = 0;
                }
                char var_36 = (var_35 == 0);
                if (var_36) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_37 = (var_33 + var_9);
                float var_38 = var_1[var_37];
                float var_39 = (var_27 + var_38);
                var_23[0] = var_28;
                var_24[0] = var_39;
            }
            long long int var_40 = var_23[0];
            float var_41 = var_24[0];
            __shared__ float var_42[310];
            long long int var_43[1];
            float var_44[1];
            var_43[0] = 32;
            var_44[0] = var_41;
            while (method_27(var_43, var_44)) {
                long long int var_46 = var_43[0];
                float var_47 = var_44[0];
                long long int var_48 = (var_46 / 2);
                long long int var_49 = threadIdx.y;
                char var_50 = (var_49 < var_46);
                char var_53;
                if (var_50) {
                    long long int var_51 = threadIdx.y;
                    var_53 = (var_51 >= var_48);
                } else {
                    var_53 = 0;
                }
                if (var_53) {
                    long long int var_54 = threadIdx.y;
                    char var_55 = (var_54 >= 1);
                    char var_57;
                    if (var_55) {
                        var_57 = (var_54 < 32);
                    } else {
                        var_57 = 0;
                    }
                    char var_58 = (var_57 == 0);
                    if (var_58) {
                        // "Argument out of bounds."
                    } else {
                    }
                    long long int var_59 = (var_54 - 1);
                    long long int var_60 = (var_59 * 10);
                    long long int var_61 = threadIdx.x;
                    char var_62 = (var_61 >= 0);
                    char var_64;
                    if (var_62) {
                        var_64 = (var_61 < 10);
                    } else {
                        var_64 = 0;
                    }
                    char var_65 = (var_64 == 0);
                    if (var_65) {
                        // "Argument out of bounds."
                    } else {
                    }
                    long long int var_66 = (var_60 + var_61);
                    var_42[var_66] = var_47;
                } else {
                }
                __syncthreads();
                long long int var_67 = threadIdx.y;
                char var_68 = (var_67 < var_48);
                float var_93;
                if (var_68) {
                    long long int var_69 = threadIdx.y;
                    long long int var_70 = (var_69 + var_48);
                    long long int var_71[1];
                    float var_72[1];
                    var_71[0] = var_70;
                    var_72[0] = var_47;
                    while (method_21(var_46, var_71, var_72)) {
                        long long int var_74 = var_71[0];
                        float var_75 = var_72[0];
                        long long int var_76 = (var_74 + var_48);
                        char var_77 = (var_74 >= 1);
                        char var_79;
                        if (var_77) {
                            var_79 = (var_74 < 32);
                        } else {
                            var_79 = 0;
                        }
                        char var_80 = (var_79 == 0);
                        if (var_80) {
                            // "Argument out of bounds."
                        } else {
                        }
                        long long int var_81 = (var_74 - 1);
                        long long int var_82 = (var_81 * 10);
                        long long int var_83 = threadIdx.x;
                        char var_84 = (var_83 >= 0);
                        char var_86;
                        if (var_84) {
                            var_86 = (var_83 < 10);
                        } else {
                            var_86 = 0;
                        }
                        char var_87 = (var_86 == 0);
                        if (var_87) {
                            // "Argument out of bounds."
                        } else {
                        }
                        long long int var_88 = (var_82 + var_83);
                        float var_89 = var_42[var_88];
                        float var_90 = (var_75 + var_89);
                        var_71[0] = var_76;
                        var_72[0] = var_90;
                    }
                    long long int var_91 = var_71[0];
                    var_93 = var_72[0];
                } else {
                    var_93 = var_47;
                }
                var_43[0] = var_48;
                var_44[0] = var_93;
            }
            long long int var_94 = var_43[0];
            float var_95 = var_44[0];
            long long int var_96 = threadIdx.y;
            char var_97 = (var_96 == 0);
            if (var_97) {
                float var_98 = var_2[var_9];
                float var_99 = (var_95 + var_98);
                var_2[var_9] = var_99;
            } else {
            }
            var_7[0] = var_10;
        }
        long long int var_100 = var_7[0];
    }
    __global__ void method_28(float * var_0, float * var_1) {
        long long int var_2 = threadIdx.x;
        long long int var_3 = blockIdx.x;
        long long int var_4 = (128 * var_3);
        long long int var_5 = (var_2 + var_4);
        long long int var_6[1];
        var_6[0] = var_5;
        while (method_29(var_6)) {
            long long int var_8 = var_6[0];
            long long int var_9 = (var_8 + 7936);
            char var_10 = (var_8 >= 0);
            char var_12;
            if (var_10) {
                var_12 = (var_8 < 7840);
            } else {
                var_12 = 0;
            }
            char var_13 = (var_12 == 0);
            if (var_13) {
                // "Argument out of bounds."
            } else {
            }
            char var_15;
            if (var_10) {
                var_15 = (var_8 < 7840);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // "Argument out of bounds."
            } else {
            }
            float var_17 = var_0[var_8];
            float var_18 = var_1[var_8];
            float var_19 = (0.25 * var_17);
            float var_20 = (var_18 - var_19);
            var_1[var_8] = var_20;
            var_6[0] = var_9;
        }
        long long int var_21 = var_6[0];
    }
    __global__ void method_30(float * var_0, float * var_1) {
        long long int var_2 = threadIdx.x;
        long long int var_3 = blockIdx.x;
        long long int var_4 = (128 * var_3);
        long long int var_5 = (var_2 + var_4);
        long long int var_6[1];
        var_6[0] = var_5;
        while (method_17(var_6)) {
            long long int var_8 = var_6[0];
            long long int var_9 = (var_8 + 128);
            char var_10 = (var_8 >= 0);
            char var_12;
            if (var_10) {
                var_12 = (var_8 < 10);
            } else {
                var_12 = 0;
            }
            char var_13 = (var_12 == 0);
            if (var_13) {
                // "Argument out of bounds."
            } else {
            }
            char var_15;
            if (var_10) {
                var_15 = (var_8 < 10);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // "Argument out of bounds."
            } else {
            }
            float var_17 = var_0[var_8];
            float var_18 = var_1[var_8];
            float var_19 = (0.25 * var_17);
            float var_20 = (var_18 - var_19);
            var_1[var_8] = var_20;
            var_6[0] = var_9;
        }
        long long int var_21 = var_6[0];
    }
    __global__ void method_33(float * var_0, float * var_1, float * var_2) {
        long long int var_3 = threadIdx.x;
        long long int var_4 = blockIdx.x;
        long long int var_5 = (10 * var_4);
        long long int var_6 = (var_3 + var_5);
        long long int var_7[1];
        var_7[0] = var_6;
        while (method_17(var_7)) {
            long long int var_9 = var_7[0];
            long long int var_10 = (var_9 + 10);
            char var_11 = (var_9 >= 0);
            char var_13;
            if (var_11) {
                var_13 = (var_9 < 10);
            } else {
                var_13 = 0;
            }
            char var_14 = (var_13 == 0);
            if (var_14) {
                // "Argument out of bounds."
            } else {
            }
            long long int var_15 = threadIdx.y;
            long long int var_16 = blockIdx.y;
            long long int var_17 = (32 * var_16);
            long long int var_18 = (var_15 + var_17);
            long long int var_19[1];
            var_19[0] = var_18;
            while (method_34(var_19)) {
                long long int var_21 = var_19[0];
                long long int var_22 = (var_21 + 32);
                char var_23 = (var_21 >= 0);
                char var_25;
                if (var_23) {
                    var_25 = (var_21 < 10000);
                } else {
                    var_25 = 0;
                }
                char var_26 = (var_25 == 0);
                if (var_26) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_27 = (var_21 * 10);
                char var_29;
                if (var_11) {
                    var_29 = (var_9 < 10);
                } else {
                    var_29 = 0;
                }
                char var_30 = (var_29 == 0);
                if (var_30) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_31 = (var_27 + var_9);
                char var_33;
                if (var_23) {
                    var_33 = (var_21 < 10000);
                } else {
                    var_33 = 0;
                }
                char var_34 = (var_33 == 0);
                if (var_34) {
                    // "Argument out of bounds."
                } else {
                }
                char var_36;
                if (var_11) {
                    var_36 = (var_9 < 10);
                } else {
                    var_36 = 0;
                }
                char var_37 = (var_36 == 0);
                if (var_37) {
                    // "Argument out of bounds."
                } else {
                }
                float var_38 = var_0[var_9];
                float var_39 = var_1[var_31];
                float var_40 = var_2[var_31];
                float var_41 = (var_38 + var_39);
                var_2[var_31] = var_41;
                var_19[0] = var_22;
            }
            long long int var_42 = var_19[0];
            var_7[0] = var_10;
        }
        long long int var_43 = var_7[0];
    }
    __global__ void method_35(float * var_0, float * var_1) {
        long long int var_2 = threadIdx.x;
        long long int var_3 = blockIdx.x;
        long long int var_4 = (128 * var_3);
        long long int var_5 = (var_2 + var_4);
        long long int var_6[1];
        var_6[0] = var_5;
        while (method_36(var_6)) {
            long long int var_8 = var_6[0];
            long long int var_9 = (var_8 + 8192);
            char var_10 = (var_8 >= 0);
            char var_12;
            if (var_10) {
                var_12 = (var_8 < 100000);
            } else {
                var_12 = 0;
            }
            char var_13 = (var_12 == 0);
            if (var_13) {
                // "Argument out of bounds."
            } else {
            }
            char var_15;
            if (var_10) {
                var_15 = (var_8 < 100000);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // "Argument out of bounds."
            } else {
            }
            float var_17 = var_0[var_8];
            float var_18 = var_1[var_8];
            float var_19 = (-var_17);
            float var_20 = exp(var_19);
            float var_21 = (1 + var_20);
            float var_22 = (1 / var_21);
            var_1[var_8] = var_22;
            var_6[0] = var_9;
        }
        long long int var_23 = var_6[0];
    }
    __global__ void method_37(float * var_0, float * var_1, float * var_2) {
        long long int var_3 = threadIdx.x;
        long long int var_4 = blockIdx.x;
        long long int var_5 = (256 * var_4);
        long long int var_6 = (var_3 + var_5);
        float var_7 = 0;
        long long int var_8[1];
        float var_9[1];
        var_8[0] = var_6;
        var_9[0] = var_7;
        while (method_38(var_8, var_9)) {
            long long int var_11 = var_8[0];
            float var_12 = var_9[0];
            long long int var_13 = (var_11 + 16384);
            char var_14 = (var_11 >= 0);
            char var_16;
            if (var_14) {
                var_16 = (var_11 < 100000);
            } else {
                var_16 = 0;
            }
            char var_17 = (var_16 == 0);
            if (var_17) {
                // "Argument out of bounds."
            } else {
            }
            float var_18 = var_0[var_11];
            float var_19 = var_1[var_11];
            float var_20 = log(var_18);
            float var_21 = (var_19 * var_20);
            float var_22 = (1 - var_19);
            float var_23 = (1 - var_18);
            float var_24 = log(var_23);
            float var_25 = (var_22 * var_24);
            float var_26 = (var_21 + var_25);
            float var_27 = (-var_26);
            float var_28 = (var_12 + var_27);
            var_8[0] = var_13;
            var_9[0] = var_28;
        }
        long long int var_29 = var_8[0];
        float var_30 = var_9[0];
        float var_31 = cub::BlockReduce<float,256,cub::BLOCK_REDUCE_WARP_REDUCTIONS,1,1>().Sum(var_30);
        long long int var_32 = threadIdx.x;
        char var_33 = (var_32 == 0);
        if (var_33) {
            long long int var_34 = blockIdx.x;
            char var_35 = (var_34 >= 0);
            char var_37;
            if (var_35) {
                var_37 = (var_34 < 64);
            } else {
                var_37 = 0;
            }
            char var_38 = (var_37 == 0);
            if (var_38) {
                // "Argument out of bounds."
            } else {
            }
            var_2[var_34] = var_31;
        } else {
        }
    }
    __global__ void method_40(float * var_0, float * var_1, float * var_2) {
        long long int var_3 = threadIdx.y;
        long long int var_4 = blockIdx.y;
        long long int var_5 = (var_3 + var_4);
        long long int var_6[1];
        var_6[0] = var_5;
        while (method_34(var_6)) {
            long long int var_8 = var_6[0];
            long long int var_9 = (var_8 + 64);
            char var_10 = (var_8 >= 0);
            char var_12;
            if (var_10) {
                var_12 = (var_8 < 10000);
            } else {
                var_12 = 0;
            }
            char var_13 = (var_12 == 0);
            if (var_13) {
                // "Argument out of bounds."
            } else {
            }
            long long int var_14 = (var_8 * 10);
            char var_16;
            if (var_10) {
                var_16 = (var_8 < 10000);
            } else {
                var_16 = 0;
            }
            char var_17 = (var_16 == 0);
            if (var_17) {
                // "Argument out of bounds."
            } else {
            }
            long long int var_18 = threadIdx.x;
            long long int var_19 = blockIdx.x;
            long long int var_20 = (10 * var_19);
            long long int var_21 = (var_18 + var_20);
            float var_22 = __int_as_float(0xff800000);
            float var_23 = 0;
            long long int var_24[1];
            float var_25[1];
            float var_26[1];
            var_24[0] = var_21;
            var_25[0] = var_22;
            var_26[0] = var_23;
            while (method_41(var_24, var_25, var_26)) {
                long long int var_28 = var_24[0];
                float var_29 = var_25[0];
                float var_30 = var_26[0];
                long long int var_31 = (var_28 + 10);
                char var_32 = (var_28 >= 0);
                char var_34;
                if (var_32) {
                    var_34 = (var_28 < 10);
                } else {
                    var_34 = 0;
                }
                char var_35 = (var_34 == 0);
                if (var_35) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_36 = (var_14 + var_28);
                float var_37 = var_0[var_36];
                float var_38 = var_1[var_36];
                char var_39 = (var_29 > var_37);
                Tuple0 var_40;
                if (var_39) {
                    var_40 = make_Tuple0(var_29, var_30);
                } else {
                    var_40 = make_Tuple0(var_37, var_38);
                }
                float var_41 = var_40.mem_0;
                float var_42 = var_40.mem_1;
                var_24[0] = var_31;
                var_25[0] = var_41;
                var_26[0] = var_42;
            }
            long long int var_43 = var_24[0];
            float var_44 = var_25[0];
            float var_45 = var_26[0];
            FunPointer1 var_48 = method_42;
            Tuple0 var_49 = cub::BlockReduce<Tuple0,10,cub::BLOCK_REDUCE_WARP_REDUCTIONS,1,1>().Reduce(make_Tuple0(var_44, var_45), var_48);
            float var_50 = var_49.mem_0;
            float var_51 = var_49.mem_1;
            long long int var_52 = threadIdx.x;
            char var_53 = (var_52 == 0);
            if (var_53) {
                char var_55;
                if (var_10) {
                    var_55 = (var_8 < 10000);
                } else {
                    var_55 = 0;
                }
                char var_56 = (var_55 == 0);
                if (var_56) {
                    // "Argument out of bounds."
                } else {
                }
                float var_57 = var_2[var_8];
                var_2[var_8] = var_51;
            } else {
            }
            var_6[0] = var_9;
        }
        long long int var_58 = var_6[0];
    }
    __device__ char method_17(long long int * var_0) {
        long long int var_1 = var_0[0];
        return (var_1 < 10);
    }
    __device__ char method_18(long long int var_0, long long int * var_1) {
        long long int var_2 = var_1[0];
        return (var_2 < var_0);
    }
    __device__ char method_21(long long int var_0, long long int * var_1, float * var_2) {
        long long int var_3 = var_1[0];
        float var_4 = var_2[0];
        return (var_3 < var_0);
    }
    __device__ char method_27(long long int * var_0, float * var_1) {
        long long int var_2 = var_0[0];
        float var_3 = var_1[0];
        return (var_2 >= 2);
    }
    __device__ char method_29(long long int * var_0) {
        long long int var_1 = var_0[0];
        return (var_1 < 7840);
    }
    __device__ char method_34(long long int * var_0) {
        long long int var_1 = var_0[0];
        return (var_1 < 10000);
    }
    __device__ char method_36(long long int * var_0) {
        long long int var_1 = var_0[0];
        return (var_1 < 100000);
    }
    __device__ char method_38(long long int * var_0, float * var_1) {
        long long int var_2 = var_0[0];
        float var_3 = var_1[0];
        return (var_2 < 100000);
    }
    __device__ char method_41(long long int * var_0, float * var_1, float * var_2) {
        long long int var_3 = var_0[0];
        float var_4 = var_1[0];
        float var_5 = var_2[0];
        return (var_3 < 10);
    }
    __device__ Tuple0 method_42(Tuple0 var_0, Tuple0 var_1) {
        float var_2 = var_0.mem_0;
        float var_3 = var_0.mem_1;
        float var_4 = var_1.mem_0;
        float var_5 = var_1.mem_1;
        char var_6 = (var_2 > var_4);
        Tuple0 var_7;
        if (var_6) {
            var_7 = make_Tuple0(var_2, var_3);
        } else {
            var_7 = make_Tuple0(var_4, var_5);
        }
        float var_8 = var_7.mem_0;
        float var_9 = var_7.mem_1;
        return make_Tuple0(var_8, var_9);
    }
}
"""

type Union0 =
    | Union0Case0 of Tuple1
    | Union0Case1
and Tuple1 =
    struct
    val mem_0: ManagedCuda.BasicTypes.CUdeviceptr
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and EnvStack2 =
    struct
    val mem_0: (Union0 ref)
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Env3 =
    struct
    val mem_0: EnvStack2
    val mem_1: int64
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and Tuple4 =
    struct
    val mem_0: Tuple5
    val mem_1: (uint8 [])
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and Tuple5 =
    struct
    val mem_0: int64
    val mem_1: int64
    val mem_2: int64
    new(arg_mem_0, arg_mem_1, arg_mem_2) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1; mem_2 = arg_mem_2}
    end
and Tuple6 =
    struct
    val mem_0: int64
    val mem_1: (uint8 [])
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and Env7 =
    struct
    val mem_0: float
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Env8 =
    struct
    val mem_0: int64
    val mem_1: float
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
let rec method_0 ((var_0: System.Diagnostics.DataReceivedEventArgs)): unit =
    let (var_1: string) = var_0.get_Data()
    let (var_2: string) = System.String.Format("{0}",var_1)
    System.Console.WriteLine(var_2)
and method_1((var_0: (Union0 ref))): ManagedCuda.BasicTypes.CUdeviceptr =
    let (var_1: Union0) = (!var_0)
    match var_1 with
    | Union0Case0(var_2) ->
        var_2.mem_0
    | Union0Case1 ->
        (failwith "A Cuda memory cell that has been disposed has been tried to be accessed.")
and method_2((var_0: string)): Tuple4 =
    let (var_1: System.IO.FileMode) = System.IO.FileMode.Open
    let (var_2: System.IO.FileAccess) = System.IO.FileAccess.Read
    let (var_3: System.IO.FileShare) = System.IO.FileShare.Read
    let (var_4: System.IO.FileStream) = System.IO.File.Open(var_0, var_1, var_2, var_3)
    let (var_5: System.IO.BinaryReader) = System.IO.BinaryReader(var_4)
    let (var_6: int32) = var_5.ReadInt32()
    let (var_7: int32) = System.Net.IPAddress.NetworkToHostOrder(var_6)
    let (var_8: bool) = (var_7 = 2051)
    let (var_9: bool) = (var_8 = false)
    if var_9 then
        (failwith "Expected a 2051i32 magic number.")
    else
        ()
    let (var_10: int32) = var_5.ReadInt32()
    let (var_11: int32) = System.Net.IPAddress.NetworkToHostOrder(var_10)
    let (var_12: int32) = var_5.ReadInt32()
    let (var_13: int32) = System.Net.IPAddress.NetworkToHostOrder(var_12)
    let (var_14: int32) = var_5.ReadInt32()
    let (var_15: int32) = System.Net.IPAddress.NetworkToHostOrder(var_14)
    let (var_16: int64) = (int64 var_11)
    let (var_17: int64) = (int64 var_13)
    let (var_18: int64) = (int64 var_15)
    let (var_19: int32) = (var_11 * var_13)
    let (var_20: int32) = (var_19 * var_15)
    let (var_22: (uint8 [])) = var_5.ReadBytes(var_20)
    var_5.Dispose()
    var_4.Dispose()
    Tuple4(Tuple5(var_16, var_17, var_18), var_22)
and method_3((var_0: (uint8 [])), (var_1: (float32 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 10000L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 784L)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = 0L
        method_4((var_0: (uint8 [])), (var_6: int64), (var_1: (float32 [])), (var_7: int64))
        let (var_8: int64) = (var_2 + 1L)
        method_3((var_0: (uint8 [])), (var_1: (float32 [])), (var_8: int64))
    else
        ()
and method_5((var_0: string)): Tuple6 =
    let (var_1: System.IO.FileMode) = System.IO.FileMode.Open
    let (var_2: System.IO.FileAccess) = System.IO.FileAccess.Read
    let (var_3: System.IO.FileShare) = System.IO.FileShare.Read
    let (var_4: System.IO.FileStream) = System.IO.File.Open(var_0, var_1, var_2, var_3)
    let (var_5: System.IO.BinaryReader) = System.IO.BinaryReader(var_4)
    let (var_6: int32) = var_5.ReadInt32()
    let (var_7: int32) = System.Net.IPAddress.NetworkToHostOrder(var_6)
    let (var_8: bool) = (var_7 = 2049)
    let (var_9: bool) = (var_8 = false)
    if var_9 then
        (failwith "Expected a 2049i32 magic number.")
    else
        ()
    let (var_10: int32) = var_5.ReadInt32()
    let (var_11: int32) = System.Net.IPAddress.NetworkToHostOrder(var_10)
    let (var_12: int64) = (int64 var_11)
    let (var_14: (uint8 [])) = var_5.ReadBytes(var_11)
    var_5.Dispose()
    var_4.Dispose()
    Tuple6(var_12, var_14)
and method_6((var_0: (uint8 [])), (var_1: (float32 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 10000L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 10L)
        let (var_7: uint8) = var_0.[int32 var_2]
        let (var_8: int64) = 0L
        method_7((var_7: uint8), (var_1: (float32 [])), (var_6: int64), (var_8: int64))
        let (var_9: int64) = (var_2 + 1L)
        method_6((var_0: (uint8 [])), (var_1: (float32 [])), (var_9: int64))
    else
        ()
and method_8((var_0: (uint8 [])), (var_1: (float32 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 60000L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 784L)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = 0L
        method_4((var_0: (uint8 [])), (var_6: int64), (var_1: (float32 [])), (var_7: int64))
        let (var_8: int64) = (var_2 + 1L)
        method_8((var_0: (uint8 [])), (var_1: (float32 [])), (var_8: int64))
    else
        ()
and method_9((var_0: (uint8 [])), (var_1: (float32 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 60000L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 10L)
        let (var_7: uint8) = var_0.[int32 var_2]
        let (var_8: int64) = 0L
        method_7((var_7: uint8), (var_1: (float32 [])), (var_6: int64), (var_8: int64))
        let (var_9: int64) = (var_2 + 1L)
        method_9((var_0: (uint8 [])), (var_1: (float32 [])), (var_9: int64))
    else
        ()
and method_10((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env3>), (var_2: uint64), (var_3: int64)): EnvStack2 =
    let (var_4: int32) = var_1.get_Count()
    let (var_5: bool) = (var_4 > 0)
    if var_5 then
        let (var_6: Env3) = var_1.Peek()
        let (var_7: EnvStack2) = var_6.mem_0
        let (var_8: int64) = var_6.mem_1
        let (var_9: (Union0 ref)) = var_7.mem_0
        let (var_10: Union0) = (!var_9)
        match var_10 with
        | Union0Case0(var_11) ->
            let (var_12: ManagedCuda.BasicTypes.CUdeviceptr) = var_11.mem_0
            method_11((var_12: ManagedCuda.BasicTypes.CUdeviceptr), (var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env3>), (var_7: EnvStack2), (var_8: int64))
        | Union0Case1 ->
            let (var_14: Env3) = var_1.Pop()
            let (var_15: EnvStack2) = var_14.mem_0
            let (var_16: int64) = var_14.mem_1
            method_10((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env3>), (var_2: uint64), (var_3: int64))
    else
        method_12((var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env3>))
and method_13((var_0: ManagedCuda.CudaContext), (var_1: ManagedCuda.CudaStream), (var_2: uint64), (var_3: uint64), (var_4: System.Collections.Generic.Stack<Env3>), (var_5: ManagedCuda.BasicTypes.CUmodule), (var_6: EnvStack2), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: EnvStack2), (var_14: EnvStack2), (var_15: int64)): unit =
    let (var_16: bool) = (var_15 < 10L)
    if var_16 then
        System.Console.WriteLine("Training:")
        let (var_17: float) = 0.000000
        let (var_18: int64) = 0L
        let (var_19: Env7) = method_14((var_13: EnvStack2), (var_14: EnvStack2), (var_0: ManagedCuda.CudaContext), (var_5: ManagedCuda.BasicTypes.CUmodule), (var_1: ManagedCuda.CudaStream), (var_2: uint64), (var_3: uint64), (var_4: System.Collections.Generic.Stack<Env3>), (var_6: EnvStack2), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_17: float), (var_18: int64))
        let (var_20: float) = var_19.mem_0
        System.Console.WriteLine("-----")
        System.Console.WriteLine("Batch done.")
        let (var_21: float) = (var_20 / 60000.000000)
        let (var_22: string) = System.String.Format("Average of batch costs is {0}.",var_21)
        let (var_23: string) = System.String.Format("{0}",var_22)
        System.Console.WriteLine(var_23)
        System.Console.WriteLine("-----")
        let (var_24: bool) = System.Double.IsNaN(var_21)
        if var_24 then
            System.Console.WriteLine("Training diverged. Aborting...")
        else
            System.Console.WriteLine("Test:")
            let (var_25: int64) = 0L
            let (var_26: float) = 0.000000
            let (var_27: int64) = 0L
            let (var_28: Env8) = method_31((var_11: EnvStack2), (var_12: EnvStack2), (var_0: ManagedCuda.CudaContext), (var_5: ManagedCuda.BasicTypes.CUmodule), (var_1: ManagedCuda.CudaStream), (var_2: uint64), (var_3: uint64), (var_4: System.Collections.Generic.Stack<Env3>), (var_6: EnvStack2), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_25: int64), (var_26: float), (var_27: int64))
            let (var_29: int64) = var_28.mem_0
            let (var_30: float) = var_28.mem_1
            System.Console.WriteLine("-----")
            System.Console.WriteLine("Batch done.")
            let (var_31: float) = (var_30 / 10000.000000)
            let (var_32: string) = System.String.Format("Average of batch costs is {0}.",var_31)
            let (var_33: string) = System.String.Format("{0}",var_32)
            System.Console.WriteLine(var_33)
            let (var_34: float) = (float var_29)
            let (var_35: float) = (var_34 / 10000.000000)
            let (var_36: float) = (var_35 * 100.000000)
            let (var_37: string) = System.String.Format("The accuracy of the batch is {0}/{1}({2}%). ",var_29,10000L,var_36)
            let (var_38: string) = System.String.Format("{0}",var_37)
            System.Console.WriteLine(var_38)
            System.Console.WriteLine("-----")
            let (var_39: int64) = (var_15 + 1L)
            method_13((var_0: ManagedCuda.CudaContext), (var_1: ManagedCuda.CudaStream), (var_2: uint64), (var_3: uint64), (var_4: System.Collections.Generic.Stack<Env3>), (var_5: ManagedCuda.BasicTypes.CUmodule), (var_6: EnvStack2), (var_7: EnvStack2), (var_8: ManagedCuda.CudaBlas.CudaBlasHandle), (var_9: EnvStack2), (var_10: EnvStack2), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: EnvStack2), (var_14: EnvStack2), (var_39: int64))
    else
        ()
and method_4((var_0: (uint8 [])), (var_1: int64), (var_2: (float32 [])), (var_3: int64)): unit =
    let (var_4: bool) = (var_3 < 784L)
    if var_4 then
        let (var_5: bool) = (var_3 >= 0L)
        let (var_6: bool) = (var_5 = false)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = (var_1 + var_3)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_8: uint8) = var_0.[int32 var_7]
        let (var_9: float32) = (float32 var_8)
        let (var_10: float32) = (var_9 / 255.000000f)
        var_2.[int32 var_7] <- var_10
        let (var_11: int64) = (var_3 + 1L)
        method_4((var_0: (uint8 [])), (var_1: int64), (var_2: (float32 [])), (var_11: int64))
    else
        ()
and method_7((var_0: uint8), (var_1: (float32 [])), (var_2: int64), (var_3: int64)): unit =
    let (var_4: bool) = (var_3 < 10L)
    if var_4 then
        let (var_5: bool) = (var_3 >= 0L)
        let (var_6: bool) = (var_5 = false)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = (var_2 + var_3)
        let (var_8: uint8) = (uint8 var_3)
        let (var_9: bool) = (var_8 = var_0)
        let (var_10: float32) =
            if var_9 then
                1.000000f
            else
                0.000000f
        var_1.[int32 var_7] <- var_10
        let (var_11: int64) = (var_3 + 1L)
        method_7((var_0: uint8), (var_1: (float32 [])), (var_2: int64), (var_11: int64))
    else
        ()
and method_11((var_0: ManagedCuda.BasicTypes.CUdeviceptr), (var_1: uint64), (var_2: uint64), (var_3: int64), (var_4: System.Collections.Generic.Stack<Env3>), (var_5: EnvStack2), (var_6: int64)): EnvStack2 =
    let (var_7: ManagedCuda.BasicTypes.SizeT) = var_0.Pointer
    let (var_8: uint64) = uint64 var_7
    let (var_9: uint64) = uint64 var_6
    let (var_10: int64) = (var_3 % 256L)
    let (var_11: int64) = (var_3 - var_10)
    let (var_12: int64) = (var_11 + 256L)
    let (var_13: uint64) = (var_8 + var_9)
    let (var_14: uint64) = (var_1 + var_2)
    let (var_15: uint64) = uint64 var_12
    let (var_16: uint64) = (var_14 - var_13)
    let (var_17: bool) = (var_15 <= var_16)
    let (var_18: bool) = (var_17 = false)
    if var_18 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_19: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
    let (var_20: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_19)
    let (var_21: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_20))))
    let (var_22: EnvStack2) = EnvStack2((var_21: (Union0 ref)))
    var_4.Push((Env3(var_22, var_12)))
    var_22
and method_12((var_0: uint64), (var_1: uint64), (var_2: int64), (var_3: System.Collections.Generic.Stack<Env3>)): EnvStack2 =
    let (var_4: int64) = (var_2 % 256L)
    let (var_5: int64) = (var_2 - var_4)
    let (var_6: int64) = (var_5 + 256L)
    let (var_7: uint64) = (var_0 + var_1)
    let (var_8: uint64) = uint64 var_6
    let (var_9: uint64) = (var_7 - var_0)
    let (var_10: bool) = (var_8 <= var_9)
    let (var_11: bool) = (var_10 = false)
    if var_11 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_12: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_0)
    let (var_13: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_12)
    let (var_14: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_13))))
    let (var_15: EnvStack2) = EnvStack2((var_14: (Union0 ref)))
    var_3.Push((Env3(var_15, var_6)))
    var_15
and method_14((var_0: EnvStack2), (var_1: EnvStack2), (var_2: ManagedCuda.CudaContext), (var_3: ManagedCuda.BasicTypes.CUmodule), (var_4: ManagedCuda.CudaStream), (var_5: uint64), (var_6: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_8: EnvStack2), (var_9: EnvStack2), (var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: float), (var_14: int64)): Env7 =
    let (var_15: bool) = (var_14 < 60000L)
    if var_15 then
        let (var_16: bool) = System.Double.IsNaN(var_13)
        if var_16 then
            (Env7(var_13))
        else
            let (var_17: int64) = (var_14 + 128L)
            let (var_18: bool) = (60000L > var_17)
            let (var_19: int64) =
                if var_18 then
                    var_17
                else
                    60000L
            let (var_20: bool) = (var_14 < var_19)
            let (var_21: bool) = (var_20 = false)
            if var_21 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_22: bool) = (var_14 >= 0L)
            let (var_23: bool) = (var_22 = false)
            if var_23 then
                (failwith "Lower boundary out of bounds.")
            else
                ()
            let (var_24: bool) = (var_19 > 0L)
            let (var_26: bool) =
                if var_24 then
                    (var_19 <= 60000L)
                else
                    false
            let (var_27: bool) = (var_26 = false)
            if var_27 then
                (failwith "Higher boundary out of bounds.")
            else
                ()
            let (var_28: int64) = (var_19 - var_14)
            let (var_29: int64) = (784L * var_14)
            if var_21 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            if var_23 then
                (failwith "Lower boundary out of bounds.")
            else
                ()
            let (var_31: bool) =
                if var_24 then
                    (var_19 <= 60000L)
                else
                    false
            let (var_32: bool) = (var_31 = false)
            if var_32 then
                (failwith "Higher boundary out of bounds.")
            else
                ()
            let (var_33: int64) = (10L * var_14)
            let (var_34: bool) = (var_28 > 0L)
            let (var_35: bool) = (var_34 = false)
            if var_35 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_36: int64) = (var_28 * 10L)
            let (var_37: int64) = (var_36 * 4L)
            let (var_38: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_37: int64))
            let (var_39: int32) = (int32 var_28)
            method_15((var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_39: int32), (var_12: EnvStack2), (var_0: EnvStack2), (var_29: int64), (var_28: int64), (var_38: EnvStack2))
            let (var_40: bool) = (0L < var_28)
            let (var_41: bool) = (var_40 = false)
            if var_41 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_42: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_37: int64))
            let (var_43: (Union0 ref)) = var_42.mem_0
            let (var_44: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_43: (Union0 ref)))
            let (var_45: ManagedCuda.BasicTypes.SizeT) = var_44.Pointer
            let (var_46: uint64) = uint64 var_45
            let (var_47: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_46)
            let (var_48: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_47)
            let (var_49: bool) = (var_36 > 0L)
            let (var_50: bool) = (var_49 = false)
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_51: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_52: int64) = (10L * var_28)
            let (var_53: int64) = (var_52 * 4L)
            let (var_54: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_53)
            var_2.ClearMemoryAsync(var_48, 0uy, var_54, var_51)
            let (var_55: bool) = (32L > var_28)
            let (var_56: int64) =
                if var_55 then
                    var_28
                else
                    32L
            let (var_57: (Union0 ref)) = var_9.mem_0
            let (var_58: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_57: (Union0 ref)))
            let (var_59: ManagedCuda.BasicTypes.SizeT) = var_58.Pointer
            let (var_60: uint64) = uint64 var_59
            let (var_61: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_60)
            let (var_62: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_61)
            let (var_63: (Union0 ref)) = var_38.mem_0
            let (var_64: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_63: (Union0 ref)))
            let (var_65: ManagedCuda.BasicTypes.SizeT) = var_64.Pointer
            let (var_66: uint64) = uint64 var_65
            let (var_67: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_66)
            let (var_68: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_67)
            let (var_69: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_63: (Union0 ref)))
            let (var_70: ManagedCuda.BasicTypes.SizeT) = var_69.Pointer
            let (var_71: uint64) = uint64 var_70
            let (var_72: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_71)
            let (var_73: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_72)
            // Cuda join point
            // method_16((var_28: int64), (var_62: ManagedCuda.BasicTypes.CUdeviceptr), (var_68: ManagedCuda.BasicTypes.CUdeviceptr), (var_73: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_74: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_16", var_3, var_2)
            let (var_75: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
            var_74.set_GridDimensions(var_75)
            let (var_76: uint32) = (uint32 var_56)
            let (var_77: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(10u, var_76, 1u)
            var_74.set_BlockDimensions(var_77)
            let (var_78: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_80: (System.Object [])) = [|var_28; var_62; var_68; var_73|]: (System.Object [])
            var_74.RunAsync(var_78, var_80)
            if var_41 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_85: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_37: int64))
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_86: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_63: (Union0 ref)))
            let (var_87: ManagedCuda.BasicTypes.SizeT) = var_86.Pointer
            let (var_88: uint64) = uint64 var_87
            let (var_89: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_88)
            let (var_90: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_89)
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_91: (Union0 ref)) = var_85.mem_0
            let (var_92: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_91: (Union0 ref)))
            let (var_93: ManagedCuda.BasicTypes.SizeT) = var_92.Pointer
            let (var_94: uint64) = uint64 var_93
            let (var_95: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_94)
            let (var_96: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_95)
            let (var_97: int64) = (var_36 - 1L)
            let (var_98: int64) = (var_97 / 128L)
            let (var_99: int64) = (var_98 + 1L)
            let (var_100: bool) = (64L > var_99)
            let (var_101: int64) =
                if var_100 then
                    var_99
                else
                    64L
            // Cuda join point
            // method_19((var_90: ManagedCuda.BasicTypes.CUdeviceptr), (var_36: int64), (var_96: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_102: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_19", var_3, var_2)
            let (var_103: uint32) = (uint32 var_101)
            let (var_104: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(var_103, 1u, 1u)
            var_102.set_GridDimensions(var_104)
            let (var_105: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
            var_102.set_BlockDimensions(var_105)
            let (var_106: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_108: (System.Object [])) = [|var_90; var_36; var_96|]: (System.Object [])
            var_102.RunAsync(var_106, var_108)
            if var_41 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_109: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_37: int64))
            let (var_110: (Union0 ref)) = var_109.mem_0
            let (var_111: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_110: (Union0 ref)))
            let (var_112: ManagedCuda.BasicTypes.SizeT) = var_111.Pointer
            let (var_113: uint64) = uint64 var_112
            let (var_114: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_113)
            let (var_115: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_114)
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_116: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_117: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_53)
            var_2.ClearMemoryAsync(var_115, 0uy, var_117, var_116)
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_118: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_91: (Union0 ref)))
            let (var_119: ManagedCuda.BasicTypes.SizeT) = var_118.Pointer
            let (var_120: uint64) = uint64 var_119
            let (var_121: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_120)
            let (var_122: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_121)
            let (var_123: (Union0 ref)) = var_1.mem_0
            let (var_124: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_123: (Union0 ref)))
            let (var_125: ManagedCuda.BasicTypes.SizeT) = var_124.Pointer
            let (var_126: uint64) = uint64 var_125
            let (var_127: int64) = (var_33 * 4L)
            let (var_128: uint64) = (uint64 var_127)
            let (var_129: uint64) = (var_126 + var_128)
            let (var_130: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_129)
            let (var_131: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_130)
            let (var_132: int64) = (var_97 / 256L)
            let (var_133: int64) = (var_132 + 1L)
            let (var_134: bool) = (64L > var_133)
            let (var_135: int64) =
                if var_134 then
                    var_133
                else
                    64L
            let (var_143: bool) = (var_135 > 0L)
            let (var_144: bool) = (var_143 = false)
            if var_144 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_145: int64) = (var_135 * 4L)
            let (var_146: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_145: int64))
            let (var_147: (Union0 ref)) = var_146.mem_0
            let (var_148: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_147: (Union0 ref)))
            let (var_149: ManagedCuda.BasicTypes.SizeT) = var_148.Pointer
            let (var_150: uint64) = uint64 var_149
            let (var_151: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_150)
            let (var_152: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_151)
            // Cuda join point
            // method_20((var_122: ManagedCuda.BasicTypes.CUdeviceptr), (var_131: ManagedCuda.BasicTypes.CUdeviceptr), (var_36: int64), (var_152: ManagedCuda.BasicTypes.CUdeviceptr), (var_135: int64))
            let (var_153: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_20", var_3, var_2)
            let (var_154: uint32) = (uint32 var_135)
            let (var_155: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(var_154, 1u, 1u)
            var_153.set_GridDimensions(var_155)
            let (var_156: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(256u, 1u, 1u)
            var_153.set_BlockDimensions(var_156)
            let (var_157: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_159: (System.Object [])) = [|var_122; var_131; var_36; var_152; var_135|]: (System.Object [])
            var_153.RunAsync(var_157, var_159)
            if var_144 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_160: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(var_135))
            let (var_161: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_147: (Union0 ref)))
            var_2.CopyToHost(var_160, var_161)
            let (var_162: bool) = (0L < var_135)
            let (var_163: bool) = (var_162 = false)
            if var_163 then
                (failwith "Argument out of bounds.")
            else
                ()
            let (var_164: float32) = var_160.[int32 0L]
            let (var_165: int64) = 1L
            let (var_166: float32) = method_22((var_160: (float32 [])), (var_135: int64), (var_164: float32), (var_165: int64))
            var_147 := Union0Case1
            let (var_167: (float32 ref)) = (ref 0.000000f)
            let (var_168: float32) = (float32 var_28)
            let (var_169: float32) = (var_166 / var_168)
            let (var_170: (float32 ref)) = (ref 0.000000f)
            var_170 := 1.000000f
            let (var_171: float32) = (!var_170)
            let (var_172: float32) = (var_171 / var_168)
            let (var_173: float32) = (!var_167)
            let (var_174: float32) = (var_173 + var_172)
            var_167 := var_174
            let (var_175: float32) = (!var_167)
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_176: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_91: (Union0 ref)))
            let (var_177: ManagedCuda.BasicTypes.SizeT) = var_176.Pointer
            let (var_178: uint64) = uint64 var_177
            let (var_179: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_178)
            let (var_180: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_179)
            let (var_181: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_123: (Union0 ref)))
            let (var_182: ManagedCuda.BasicTypes.SizeT) = var_181.Pointer
            let (var_183: uint64) = uint64 var_182
            let (var_184: uint64) = (var_183 + var_128)
            let (var_185: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_184)
            let (var_186: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_185)
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_187: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_110: (Union0 ref)))
            let (var_188: ManagedCuda.BasicTypes.SizeT) = var_187.Pointer
            let (var_189: uint64) = uint64 var_188
            let (var_190: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_189)
            let (var_191: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_190)
            let (var_192: int64) =
                if var_100 then
                    var_99
                else
                    64L
            // Cuda join point
            // method_23((var_175: float32), (var_166: float32), (var_180: ManagedCuda.BasicTypes.CUdeviceptr), (var_186: ManagedCuda.BasicTypes.CUdeviceptr), (var_36: int64), (var_191: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_193: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_23", var_3, var_2)
            let (var_194: uint32) = (uint32 var_192)
            let (var_195: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(var_194, 1u, 1u)
            var_193.set_GridDimensions(var_195)
            let (var_196: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
            var_193.set_BlockDimensions(var_196)
            let (var_197: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_199: (System.Object [])) = [|var_175; var_166; var_180; var_186; var_36; var_191|]: (System.Object [])
            var_193.RunAsync(var_197, var_199)
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_200: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_63: (Union0 ref)))
            let (var_201: ManagedCuda.BasicTypes.SizeT) = var_200.Pointer
            let (var_202: uint64) = uint64 var_201
            let (var_203: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_202)
            let (var_204: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_203)
            let (var_205: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_110: (Union0 ref)))
            let (var_206: ManagedCuda.BasicTypes.SizeT) = var_205.Pointer
            let (var_207: uint64) = uint64 var_206
            let (var_208: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_207)
            let (var_209: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_208)
            let (var_210: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_91: (Union0 ref)))
            let (var_211: ManagedCuda.BasicTypes.SizeT) = var_210.Pointer
            let (var_212: uint64) = uint64 var_211
            let (var_213: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_212)
            let (var_214: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_213)
            if var_50 then
                (failwith "Tensor needs to be at least size 1.")
            else
                ()
            let (var_215: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_43: (Union0 ref)))
            let (var_216: ManagedCuda.BasicTypes.SizeT) = var_215.Pointer
            let (var_217: uint64) = uint64 var_216
            let (var_218: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_217)
            let (var_219: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_218)
            let (var_220: int64) =
                if var_100 then
                    var_99
                else
                    64L
            // Cuda join point
            // method_24((var_204: ManagedCuda.BasicTypes.CUdeviceptr), (var_209: ManagedCuda.BasicTypes.CUdeviceptr), (var_214: ManagedCuda.BasicTypes.CUdeviceptr), (var_36: int64), (var_219: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_221: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_24", var_3, var_2)
            let (var_222: uint32) = (uint32 var_220)
            let (var_223: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(var_222, 1u, 1u)
            var_221.set_GridDimensions(var_223)
            let (var_224: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
            var_221.set_BlockDimensions(var_224)
            let (var_225: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_227: (System.Object [])) = [|var_204; var_209; var_214; var_36; var_219|]: (System.Object [])
            var_221.RunAsync(var_225, var_227)
            method_25((var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_39: int32), (var_42: EnvStack2), (var_28: int64), (var_0: EnvStack2), (var_29: int64), (var_11: EnvStack2))
            let (var_228: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_43: (Union0 ref)))
            let (var_229: ManagedCuda.BasicTypes.SizeT) = var_228.Pointer
            let (var_230: uint64) = uint64 var_229
            let (var_231: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_230)
            let (var_232: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_231)
            let (var_233: (Union0 ref)) = var_8.mem_0
            let (var_234: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_233: (Union0 ref)))
            let (var_235: ManagedCuda.BasicTypes.SizeT) = var_234.Pointer
            let (var_236: uint64) = uint64 var_235
            let (var_237: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_236)
            let (var_238: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_237)
            // Cuda join point
            // method_26((var_28: int64), (var_232: ManagedCuda.BasicTypes.CUdeviceptr), (var_238: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_239: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_26", var_3, var_2)
            let (var_240: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
            var_239.set_GridDimensions(var_240)
            let (var_241: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(10u, 32u, 1u)
            var_239.set_BlockDimensions(var_241)
            let (var_242: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_244: (System.Object [])) = [|var_28; var_232; var_238|]: (System.Object [])
            var_239.RunAsync(var_242, var_244)
            let (var_245: (Union0 ref)) = var_11.mem_0
            let (var_246: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_245: (Union0 ref)))
            let (var_247: ManagedCuda.BasicTypes.SizeT) = var_246.Pointer
            let (var_248: uint64) = uint64 var_247
            let (var_249: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_248)
            let (var_250: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_249)
            let (var_251: (Union0 ref)) = var_12.mem_0
            let (var_252: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_251: (Union0 ref)))
            let (var_253: ManagedCuda.BasicTypes.SizeT) = var_252.Pointer
            let (var_254: uint64) = uint64 var_253
            let (var_255: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_254)
            let (var_256: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_255)
            // Cuda join point
            // method_28((var_250: ManagedCuda.BasicTypes.CUdeviceptr), (var_256: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_257: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_28", var_3, var_2)
            let (var_258: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(62u, 1u, 1u)
            var_257.set_GridDimensions(var_258)
            let (var_259: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
            var_257.set_BlockDimensions(var_259)
            let (var_260: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_262: (System.Object [])) = [|var_250; var_256|]: (System.Object [])
            var_257.RunAsync(var_260, var_262)
            let (var_263: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_245: (Union0 ref)))
            let (var_264: ManagedCuda.BasicTypes.SizeT) = var_263.Pointer
            let (var_265: uint64) = uint64 var_264
            let (var_266: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_265)
            let (var_267: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_266)
            let (var_268: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_269: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(31360L)
            var_2.ClearMemoryAsync(var_267, 0uy, var_269, var_268)
            let (var_270: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_233: (Union0 ref)))
            let (var_271: ManagedCuda.BasicTypes.SizeT) = var_270.Pointer
            let (var_272: uint64) = uint64 var_271
            let (var_273: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_272)
            let (var_274: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_273)
            let (var_275: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_57: (Union0 ref)))
            let (var_276: ManagedCuda.BasicTypes.SizeT) = var_275.Pointer
            let (var_277: uint64) = uint64 var_276
            let (var_278: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_277)
            let (var_279: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_278)
            // Cuda join point
            // method_30((var_274: ManagedCuda.BasicTypes.CUdeviceptr), (var_279: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_280: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_30", var_3, var_2)
            let (var_281: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
            var_280.set_GridDimensions(var_281)
            let (var_282: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
            var_280.set_BlockDimensions(var_282)
            let (var_283: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_285: (System.Object [])) = [|var_274; var_279|]: (System.Object [])
            var_280.RunAsync(var_283, var_285)
            let (var_286: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_233: (Union0 ref)))
            let (var_287: ManagedCuda.BasicTypes.SizeT) = var_286.Pointer
            let (var_288: uint64) = uint64 var_287
            let (var_289: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_288)
            let (var_290: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_289)
            let (var_291: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_292: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(40L)
            var_2.ClearMemoryAsync(var_290, 0uy, var_292, var_291)
            let (var_293: float) = (float var_169)
            let (var_294: float) = (float var_28)
            let (var_295: float) = (var_293 * var_294)
            let (var_296: float) = (var_13 + var_295)
            var_110 := Union0Case1
            var_91 := Union0Case1
            var_43 := Union0Case1
            var_63 := Union0Case1
            method_14((var_0: EnvStack2), (var_1: EnvStack2), (var_2: ManagedCuda.CudaContext), (var_3: ManagedCuda.BasicTypes.CUmodule), (var_4: ManagedCuda.CudaStream), (var_5: uint64), (var_6: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_8: EnvStack2), (var_9: EnvStack2), (var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_11: EnvStack2), (var_12: EnvStack2), (var_296: float), (var_17: int64))
    else
        (Env7(var_13))
and method_31((var_0: EnvStack2), (var_1: EnvStack2), (var_2: ManagedCuda.CudaContext), (var_3: ManagedCuda.BasicTypes.CUmodule), (var_4: ManagedCuda.CudaStream), (var_5: uint64), (var_6: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_8: EnvStack2), (var_9: EnvStack2), (var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_11: EnvStack2), (var_12: EnvStack2), (var_13: int64), (var_14: float), (var_15: int64)): Env8 =
    let (var_16: bool) = (var_15 < 10000L)
    if var_16 then
        let (var_17: bool) = System.Double.IsNaN(var_14)
        if var_17 then
            (Env8(var_13, var_14))
        else
            let (var_18: int64) = (var_15 + 10000L)
            let (var_19: bool) = (var_15 >= 0L)
            let (var_20: bool) = (var_19 = false)
            if var_20 then
                (failwith "Lower boundary out of bounds.")
            else
                ()
            let (var_21: bool) = (var_18 > 0L)
            let (var_23: bool) =
                if var_21 then
                    (var_18 <= 10000L)
                else
                    false
            let (var_24: bool) = (var_23 = false)
            if var_24 then
                (failwith "Higher boundary out of bounds.")
            else
                ()
            let (var_25: int64) = (784L * var_15)
            if var_20 then
                (failwith "Lower boundary out of bounds.")
            else
                ()
            let (var_27: bool) =
                if var_21 then
                    (var_18 <= 10000L)
                else
                    false
            let (var_28: bool) = (var_27 = false)
            if var_28 then
                (failwith "Higher boundary out of bounds.")
            else
                ()
            let (var_29: int64) = (10L * var_15)
            let (var_30: int64) = 400000L
            let (var_31: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_30: int64))
            method_32((var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_12: EnvStack2), (var_0: EnvStack2), (var_25: int64), (var_31: EnvStack2))
            let (var_32: int64) = 400000L
            let (var_33: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_32: int64))
            let (var_34: (Union0 ref)) = var_33.mem_0
            let (var_35: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_34: (Union0 ref)))
            let (var_36: ManagedCuda.BasicTypes.SizeT) = var_35.Pointer
            let (var_37: uint64) = uint64 var_36
            let (var_38: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_37)
            let (var_39: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_38)
            let (var_40: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_41: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(400000L)
            var_2.ClearMemoryAsync(var_39, 0uy, var_41, var_40)
            let (var_42: (Union0 ref)) = var_9.mem_0
            let (var_43: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_42: (Union0 ref)))
            let (var_44: ManagedCuda.BasicTypes.SizeT) = var_43.Pointer
            let (var_45: uint64) = uint64 var_44
            let (var_46: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_45)
            let (var_47: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_46)
            let (var_48: (Union0 ref)) = var_31.mem_0
            let (var_49: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_48: (Union0 ref)))
            let (var_50: ManagedCuda.BasicTypes.SizeT) = var_49.Pointer
            let (var_51: uint64) = uint64 var_50
            let (var_52: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_51)
            let (var_53: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_52)
            let (var_54: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_48: (Union0 ref)))
            let (var_55: ManagedCuda.BasicTypes.SizeT) = var_54.Pointer
            let (var_56: uint64) = uint64 var_55
            let (var_57: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_56)
            let (var_58: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_57)
            // Cuda join point
            // method_33((var_47: ManagedCuda.BasicTypes.CUdeviceptr), (var_53: ManagedCuda.BasicTypes.CUdeviceptr), (var_58: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_59: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_33", var_3, var_2)
            let (var_60: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
            var_59.set_GridDimensions(var_60)
            let (var_61: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(10u, 32u, 1u)
            var_59.set_BlockDimensions(var_61)
            let (var_62: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_64: (System.Object [])) = [|var_47; var_53; var_58|]: (System.Object [])
            var_59.RunAsync(var_62, var_64)
            let (var_69: int64) = 400000L
            let (var_70: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_69: int64))
            let (var_71: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_48: (Union0 ref)))
            let (var_72: ManagedCuda.BasicTypes.SizeT) = var_71.Pointer
            let (var_73: uint64) = uint64 var_72
            let (var_74: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_73)
            let (var_75: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_74)
            let (var_76: (Union0 ref)) = var_70.mem_0
            let (var_77: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_76: (Union0 ref)))
            let (var_78: ManagedCuda.BasicTypes.SizeT) = var_77.Pointer
            let (var_79: uint64) = uint64 var_78
            let (var_80: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_79)
            let (var_81: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_80)
            // Cuda join point
            // method_35((var_75: ManagedCuda.BasicTypes.CUdeviceptr), (var_81: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_82: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_35", var_3, var_2)
            let (var_83: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(64u, 1u, 1u)
            var_82.set_GridDimensions(var_83)
            let (var_84: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
            var_82.set_BlockDimensions(var_84)
            let (var_85: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_87: (System.Object [])) = [|var_75; var_81|]: (System.Object [])
            var_82.RunAsync(var_85, var_87)
            let (var_88: int64) = 400000L
            let (var_89: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_88: int64))
            let (var_90: (Union0 ref)) = var_89.mem_0
            let (var_91: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_90: (Union0 ref)))
            let (var_92: ManagedCuda.BasicTypes.SizeT) = var_91.Pointer
            let (var_93: uint64) = uint64 var_92
            let (var_94: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_93)
            let (var_95: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_94)
            let (var_96: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_97: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(400000L)
            var_2.ClearMemoryAsync(var_95, 0uy, var_97, var_96)
            let (var_98: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_76: (Union0 ref)))
            let (var_99: ManagedCuda.BasicTypes.SizeT) = var_98.Pointer
            let (var_100: uint64) = uint64 var_99
            let (var_101: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_100)
            let (var_102: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_101)
            let (var_103: (Union0 ref)) = var_1.mem_0
            let (var_104: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_103: (Union0 ref)))
            let (var_105: ManagedCuda.BasicTypes.SizeT) = var_104.Pointer
            let (var_106: uint64) = uint64 var_105
            let (var_107: int64) = (var_29 * 4L)
            let (var_108: uint64) = (uint64 var_107)
            let (var_109: uint64) = (var_106 + var_108)
            let (var_110: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_109)
            let (var_111: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_110)
            let (var_119: int64) = 256L
            let (var_120: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_119: int64))
            let (var_121: (Union0 ref)) = var_120.mem_0
            let (var_122: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_121: (Union0 ref)))
            let (var_123: ManagedCuda.BasicTypes.SizeT) = var_122.Pointer
            let (var_124: uint64) = uint64 var_123
            let (var_125: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_124)
            let (var_126: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_125)
            // Cuda join point
            // method_37((var_102: ManagedCuda.BasicTypes.CUdeviceptr), (var_111: ManagedCuda.BasicTypes.CUdeviceptr), (var_126: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_127: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_37", var_3, var_2)
            let (var_128: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(64u, 1u, 1u)
            var_127.set_GridDimensions(var_128)
            let (var_129: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(256u, 1u, 1u)
            var_127.set_BlockDimensions(var_129)
            let (var_130: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_132: (System.Object [])) = [|var_102; var_111; var_126|]: (System.Object [])
            var_127.RunAsync(var_130, var_132)
            let (var_133: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(64L))
            let (var_134: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_121: (Union0 ref)))
            var_2.CopyToHost(var_133, var_134)
            let (var_135: float32) = var_133.[int32 0L]
            let (var_136: int64) = 1L
            let (var_137: float32) = method_39((var_133: (float32 [])), (var_135: float32), (var_136: int64))
            var_121 := Union0Case1
            let (var_138: (float32 ref)) = (ref 0.000000f)
            let (var_139: float32) = (var_137 / 10000.000000f)
            let (var_140: (float32 ref)) = (ref 0.000000f)
            let (var_141: float) = (float var_139)
            let (var_142: float) = (var_141 * 10000.000000)
            let (var_143: float) = (var_14 + var_142)
            let (var_144: int64) = 40000L
            let (var_145: EnvStack2) = method_10((var_5: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_6: uint64), (var_144: int64))
            let (var_146: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_76: (Union0 ref)))
            let (var_147: ManagedCuda.BasicTypes.SizeT) = var_146.Pointer
            let (var_148: uint64) = uint64 var_147
            let (var_149: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_148)
            let (var_150: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_149)
            let (var_151: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_103: (Union0 ref)))
            let (var_152: ManagedCuda.BasicTypes.SizeT) = var_151.Pointer
            let (var_153: uint64) = uint64 var_152
            let (var_154: uint64) = (var_153 + var_108)
            let (var_155: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_154)
            let (var_156: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_155)
            let (var_157: (Union0 ref)) = var_145.mem_0
            let (var_158: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_157: (Union0 ref)))
            let (var_159: ManagedCuda.BasicTypes.SizeT) = var_158.Pointer
            let (var_160: uint64) = uint64 var_159
            let (var_161: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_160)
            let (var_162: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_161)
            // Cuda join point
            // method_40((var_150: ManagedCuda.BasicTypes.CUdeviceptr), (var_156: ManagedCuda.BasicTypes.CUdeviceptr), (var_162: ManagedCuda.BasicTypes.CUdeviceptr))
            let (var_163: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_40", var_3, var_2)
            let (var_164: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 64u, 1u)
            var_163.set_GridDimensions(var_164)
            let (var_165: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(10u, 1u, 1u)
            var_163.set_BlockDimensions(var_165)
            let (var_166: ManagedCuda.BasicTypes.CUstream) = var_4.get_Stream()
            let (var_168: (System.Object [])) = [|var_150; var_156; var_162|]: (System.Object [])
            var_163.RunAsync(var_166, var_168)
            let (var_169: int64) = 0L
            let (var_170: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(10000L))
            let (var_171: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_157: (Union0 ref)))
            var_2.CopyToHost(var_170, var_171)
            let (var_172: int64) = var_170.LongLength
            let (var_173: int64) = 0L
            let (var_174: int64) = method_43((var_170: (float32 [])), (var_172: int64), (var_169: int64), (var_173: int64))
            var_157 := Union0Case1
            let (var_175: int64) = (var_13 + var_174)
            var_90 := Union0Case1
            var_76 := Union0Case1
            var_34 := Union0Case1
            var_48 := Union0Case1
            method_31((var_0: EnvStack2), (var_1: EnvStack2), (var_2: ManagedCuda.CudaContext), (var_3: ManagedCuda.BasicTypes.CUmodule), (var_4: ManagedCuda.CudaStream), (var_5: uint64), (var_6: uint64), (var_7: System.Collections.Generic.Stack<Env3>), (var_8: EnvStack2), (var_9: EnvStack2), (var_10: ManagedCuda.CudaBlas.CudaBlasHandle), (var_11: EnvStack2), (var_12: EnvStack2), (var_175: int64), (var_143: float), (var_18: int64))
    else
        (Env8(var_13, var_14))
and method_15((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: int32), (var_2: EnvStack2), (var_3: EnvStack2), (var_4: int64), (var_5: int64), (var_6: EnvStack2)): unit =
    let (var_7: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_8: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_9: (float32 ref)) = (ref 1.000000f)
    let (var_10: (Union0 ref)) = var_2.mem_0
    let (var_11: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_10: (Union0 ref)))
    let (var_12: ManagedCuda.BasicTypes.SizeT) = var_11.Pointer
    let (var_13: uint64) = uint64 var_12
    let (var_14: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
    let (var_15: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_14)
    let (var_16: int64) = (var_5 * 784L)
    let (var_17: bool) = (var_16 > 0L)
    let (var_18: bool) = (var_17 = false)
    if var_18 then
        (failwith "Tensor needs to be at least size 1.")
    else
        ()
    let (var_19: (Union0 ref)) = var_3.mem_0
    let (var_20: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_19: (Union0 ref)))
    let (var_21: ManagedCuda.BasicTypes.SizeT) = var_20.Pointer
    let (var_22: uint64) = uint64 var_21
    let (var_23: int64) = (var_4 * 4L)
    let (var_24: uint64) = (uint64 var_23)
    let (var_25: uint64) = (var_22 + var_24)
    let (var_26: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_25)
    let (var_27: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_26)
    let (var_28: (float32 ref)) = (ref 0.000000f)
    let (var_29: int64) = (var_5 * 10L)
    let (var_30: bool) = (var_29 > 0L)
    let (var_31: bool) = (var_30 = false)
    if var_31 then
        (failwith "Tensor needs to be at least size 1.")
    else
        ()
    let (var_32: (Union0 ref)) = var_6.mem_0
    let (var_33: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_32: (Union0 ref)))
    let (var_34: ManagedCuda.BasicTypes.SizeT) = var_33.Pointer
    let (var_35: uint64) = uint64 var_34
    let (var_36: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_35)
    let (var_37: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_36)
    let (var_38: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_7, var_8, 10, var_1, 784, var_9, var_15, 10, var_27, 784, var_28, var_37, 10)
    if var_38 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_38)
and method_22((var_0: (float32 [])), (var_1: int64), (var_2: float32), (var_3: int64)): float32 =
    let (var_4: bool) = (var_3 < var_1)
    if var_4 then
        let (var_5: bool) = (var_3 >= 0L)
        let (var_6: bool) = (var_5 = false)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: float32) = var_0.[int32 var_3]
        let (var_8: float32) = (var_2 + var_7)
        let (var_9: int64) = (var_3 + 1L)
        method_22((var_0: (float32 [])), (var_1: int64), (var_8: float32), (var_9: int64))
    else
        var_2
and method_25((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: int32), (var_2: EnvStack2), (var_3: int64), (var_4: EnvStack2), (var_5: int64), (var_6: EnvStack2)): unit =
    let (var_7: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_8: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.Transpose
    let (var_9: (float32 ref)) = (ref 1.000000f)
    let (var_10: int64) = (var_3 * 10L)
    let (var_11: bool) = (var_10 > 0L)
    let (var_12: bool) = (var_11 = false)
    if var_12 then
        (failwith "Tensor needs to be at least size 1.")
    else
        ()
    let (var_13: (Union0 ref)) = var_2.mem_0
    let (var_14: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_13: (Union0 ref)))
    let (var_15: ManagedCuda.BasicTypes.SizeT) = var_14.Pointer
    let (var_16: uint64) = uint64 var_15
    let (var_17: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_16)
    let (var_18: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_17)
    let (var_19: int64) = (var_3 * 784L)
    let (var_20: bool) = (var_19 > 0L)
    let (var_21: bool) = (var_20 = false)
    if var_21 then
        (failwith "Tensor needs to be at least size 1.")
    else
        ()
    let (var_22: (Union0 ref)) = var_4.mem_0
    let (var_23: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_22: (Union0 ref)))
    let (var_24: ManagedCuda.BasicTypes.SizeT) = var_23.Pointer
    let (var_25: uint64) = uint64 var_24
    let (var_26: int64) = (var_5 * 4L)
    let (var_27: uint64) = (uint64 var_26)
    let (var_28: uint64) = (var_25 + var_27)
    let (var_29: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_28)
    let (var_30: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_29)
    let (var_31: (float32 ref)) = (ref 1.000000f)
    let (var_32: (Union0 ref)) = var_6.mem_0
    let (var_33: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_32: (Union0 ref)))
    let (var_34: ManagedCuda.BasicTypes.SizeT) = var_33.Pointer
    let (var_35: uint64) = uint64 var_34
    let (var_36: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_35)
    let (var_37: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_36)
    let (var_38: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_7, var_8, 10, 784, var_1, var_9, var_18, 10, var_30, 784, var_31, var_37, 10)
    if var_38 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_38)
and method_32((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: EnvStack2), (var_2: EnvStack2), (var_3: int64), (var_4: EnvStack2)): unit =
    let (var_5: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_6: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_7: (float32 ref)) = (ref 1.000000f)
    let (var_8: (Union0 ref)) = var_1.mem_0
    let (var_9: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_8: (Union0 ref)))
    let (var_10: ManagedCuda.BasicTypes.SizeT) = var_9.Pointer
    let (var_11: uint64) = uint64 var_10
    let (var_12: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_11)
    let (var_13: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_12)
    let (var_14: (Union0 ref)) = var_2.mem_0
    let (var_15: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_14: (Union0 ref)))
    let (var_16: ManagedCuda.BasicTypes.SizeT) = var_15.Pointer
    let (var_17: uint64) = uint64 var_16
    let (var_18: int64) = (var_3 * 4L)
    let (var_19: uint64) = (uint64 var_18)
    let (var_20: uint64) = (var_17 + var_19)
    let (var_21: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_20)
    let (var_22: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_21)
    let (var_23: (float32 ref)) = (ref 0.000000f)
    let (var_24: (Union0 ref)) = var_4.mem_0
    let (var_25: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_24: (Union0 ref)))
    let (var_26: ManagedCuda.BasicTypes.SizeT) = var_25.Pointer
    let (var_27: uint64) = uint64 var_26
    let (var_28: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_27)
    let (var_29: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_28)
    let (var_30: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_5, var_6, 10, 10000, 784, var_7, var_13, 10, var_22, 784, var_23, var_29, 10)
    if var_30 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_30)
and method_39((var_0: (float32 [])), (var_1: float32), (var_2: int64)): float32 =
    let (var_3: bool) = (var_2 < 64L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: float32) = var_0.[int32 var_2]
        let (var_7: float32) = (var_1 + var_6)
        let (var_8: int64) = (var_2 + 1L)
        method_39((var_0: (float32 [])), (var_7: float32), (var_8: int64))
    else
        var_1
and method_43((var_0: (float32 [])), (var_1: int64), (var_2: int64), (var_3: int64)): int64 =
    let (var_4: bool) = (var_3 < var_1)
    if var_4 then
        let (var_5: float32) = var_0.[int32 var_3]
        let (var_6: bool) = (var_5 = 1.000000f)
        let (var_8: int64) =
            if var_6 then
                (var_2 + 1L)
            else
                var_2
        let (var_9: int64) = (var_3 + 1L)
        method_43((var_0: (float32 [])), (var_1: int64), (var_8: int64), (var_9: int64))
    else
        var_2
let (var_0: string) = cuda_kernels
let (var_1: ManagedCuda.CudaContext) = ManagedCuda.CudaContext(false)
var_1.Synchronize()
let (var_2: string) = System.Environment.get_CurrentDirectory()
let (var_3: string) = System.IO.Path.Combine(var_2, "nvcc_router.bat")
let (var_4: System.Diagnostics.ProcessStartInfo) = System.Diagnostics.ProcessStartInfo()
var_4.set_RedirectStandardOutput(true)
var_4.set_RedirectStandardError(true)
var_4.set_UseShellExecute(false)
var_4.set_FileName(var_3)
let (var_5: System.Diagnostics.Process) = System.Diagnostics.Process()
var_5.set_StartInfo(var_4)
let (var_7: (System.Diagnostics.DataReceivedEventArgs -> unit)) = method_0
var_5.OutputDataReceived.Add(var_7)
var_5.ErrorDataReceived.Add(var_7)
let (var_8: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Auxiliary/Build/vcvars64.bat")
let (var_9: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Tools/MSVC/14.11.25503/bin/Hostx64/x64")
let (var_10: string) = System.IO.Path.Combine("C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v9.0", "include")
let (var_11: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Tools/MSVC/14.11.25503/include")
let (var_12: string) = System.IO.Path.Combine("C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v9.0", "bin/nvcc.exe")
let (var_13: string) = System.IO.Path.Combine(var_2, "cuda_kernels.ptx")
let (var_14: string) = System.IO.Path.Combine(var_2, "cuda_kernels.cu")
let (var_15: bool) = System.IO.File.Exists(var_14)
if var_15 then
    System.IO.File.Delete(var_14)
else
    ()
System.IO.File.WriteAllText(var_14, var_0)
let (var_16: bool) = System.IO.File.Exists(var_3)
if var_16 then
    System.IO.File.Delete(var_3)
else
    ()
let (var_17: System.IO.FileStream) = System.IO.File.OpenWrite(var_3)
let (var_18: System.IO.StreamWriter) = System.IO.StreamWriter(var_17)
var_18.WriteLine("SETLOCAL")
let (var_19: string) = String.concat "" [|"CALL "; "\""; var_8; "\""|]
var_18.WriteLine(var_19)
let (var_20: string) = String.concat "" [|"SET PATH=%PATH%;"; "\""; var_9; "\""|]
var_18.WriteLine(var_20)
let (var_21: string) = String.concat "" [|"\""; var_12; "\" -gencode=arch=compute_52,code=\\\"sm_52,compute_52\\\" --use-local-env --cl-version 2017 -I\""; var_10; "\" -I\"C:/cub-1.7.4\" -I\""; var_11; "\" --keep-dir \""; var_2; "\" -maxrregcount=0  --machine 64 -ptx -cudart static  -o \""; var_13; "\" \""; var_14; "\""|]
var_18.WriteLine(var_21)
var_18.Dispose()
var_17.Dispose()
let (var_22: System.Diagnostics.Stopwatch) = System.Diagnostics.Stopwatch.StartNew()
let (var_23: bool) = var_5.Start()
let (var_24: bool) = (var_23 = false)
if var_24 then
    (failwith "NVCC failed to run.")
else
    ()
var_5.BeginOutputReadLine()
var_5.BeginErrorReadLine()
var_5.WaitForExit()
let (var_25: int32) = var_5.get_ExitCode()
let (var_26: bool) = (var_25 = 0)
let (var_27: bool) = (var_26 = false)
if var_27 then
    let (var_28: string) = System.String.Format("{0}",var_25)
    let (var_29: string) = String.concat ", " [|"NVCC failed compilation."; var_28|]
    let (var_30: string) = System.String.Format("[{0}]",var_29)
    (failwith var_30)
else
    ()
let (var_31: System.TimeSpan) = var_22.get_Elapsed()
printfn "The time it took to compile the Cuda kernels is: %A" var_31
let (var_32: ManagedCuda.BasicTypes.CUmodule) = var_1.LoadModulePTX(var_13)
var_5.Dispose()
let (var_33: string) = String.concat "" [|"Compiled the kernels into the following directory: "; var_2|]
let (var_34: string) = System.String.Format("{0}",var_33)
System.Console.WriteLine(var_34)
let (var_35: ManagedCuda.CudaDeviceProperties) = var_1.GetDeviceInfo()
let (var_36: ManagedCuda.BasicTypes.SizeT) = var_35.get_TotalGlobalMemory()
let (var_37: int64) = int64 var_36
let (var_38: float) = float var_37
let (var_39: float) = (0.700000 * var_38)
let (var_40: int64) = int64 var_39
let (var_41: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_40)
let (var_42: ManagedCuda.BasicTypes.CUdeviceptr) = var_1.AllocateMemory(var_41)
let (var_43: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_42))))
let (var_44: EnvStack2) = EnvStack2((var_43: (Union0 ref)))
let (var_45: System.Collections.Generic.Stack<Env3>) = System.Collections.Generic.Stack<Env3>()
let (var_46: (Union0 ref)) = var_44.mem_0
let (var_47: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_46: (Union0 ref)))
let (var_48: ManagedCuda.BasicTypes.SizeT) = var_47.Pointer
let (var_49: uint64) = uint64 var_48
let (var_50: uint64) = uint64 var_40
let (var_51: ManagedCuda.CudaStream) = ManagedCuda.CudaStream()
let (var_52: ManagedCuda.CudaRand.GeneratorType) = ManagedCuda.CudaRand.GeneratorType.PseudoDefault
let (var_53: ManagedCuda.CudaRand.CudaRandDevice) = ManagedCuda.CudaRand.CudaRandDevice(var_52)
let (var_54: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
var_53.SetStream(var_54)
let (var_55: ManagedCuda.CudaBlas.PointerMode) = ManagedCuda.CudaBlas.PointerMode.Host
let (var_56: ManagedCuda.CudaBlas.AtomicsMode) = ManagedCuda.CudaBlas.AtomicsMode.Allowed
let (var_57: ManagedCuda.CudaBlas.CudaBlas) = ManagedCuda.CudaBlas.CudaBlas(var_55, var_56)
let (var_58: ManagedCuda.CudaBlas.CudaBlasHandle) = var_57.get_CublasHandle()
let (var_59: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
var_57.set_Stream(var_59)
let (var_60: string) = System.IO.Path.Combine("C:\\ML Datasets\\Mnist", "t10k-images.idx3-ubyte")
let (var_61: Tuple4) = method_2((var_60: string))
let (var_62: Tuple5) = var_61.mem_0
let (var_63: (uint8 [])) = var_61.mem_1
let (var_64: int64) = var_62.mem_0
let (var_65: int64) = var_62.mem_1
let (var_66: int64) = var_62.mem_2
let (var_67: bool) = (10000L = var_64)
let (var_71: bool) =
    if var_67 then
        let (var_68: bool) = (28L = var_65)
        if var_68 then
            (28L = var_66)
        else
            false
    else
        false
let (var_72: bool) = (var_71 = false)
if var_72 then
    (failwith "Mnist dimensions do not match the expected values.")
else
    ()
let (var_73: int64) = var_63.LongLength
let (var_74: bool) = (var_73 > 0L)
let (var_75: bool) = (var_74 = false)
if var_75 then
    (failwith "Tensor needs to be at least size 1.")
else
    ()
let (var_79: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(7840000L))
let (var_80: int64) = 0L
method_3((var_63: (uint8 [])), (var_79: (float32 [])), (var_80: int64))
let (var_81: string) = System.IO.Path.Combine("C:\\ML Datasets\\Mnist", "t10k-labels.idx1-ubyte")
let (var_82: Tuple6) = method_5((var_81: string))
let (var_83: int64) = var_82.mem_0
let (var_84: (uint8 [])) = var_82.mem_1
let (var_85: bool) = (10000L = var_83)
let (var_86: bool) = (var_85 = false)
if var_86 then
    (failwith "Mnist dimensions do not match the expected values.")
else
    ()
let (var_90: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(100000L))
let (var_91: int64) = 0L
method_6((var_84: (uint8 [])), (var_90: (float32 [])), (var_91: int64))
let (var_92: string) = System.IO.Path.Combine("C:\\ML Datasets\\Mnist", "train-images.idx3-ubyte")
let (var_93: Tuple4) = method_2((var_92: string))
let (var_94: Tuple5) = var_93.mem_0
let (var_95: (uint8 [])) = var_93.mem_1
let (var_96: int64) = var_94.mem_0
let (var_97: int64) = var_94.mem_1
let (var_98: int64) = var_94.mem_2
let (var_99: bool) = (60000L = var_96)
let (var_103: bool) =
    if var_99 then
        let (var_100: bool) = (28L = var_97)
        if var_100 then
            (28L = var_98)
        else
            false
    else
        false
let (var_104: bool) = (var_103 = false)
if var_104 then
    (failwith "Mnist dimensions do not match the expected values.")
else
    ()
let (var_105: int64) = var_95.LongLength
let (var_106: bool) = (var_105 > 0L)
let (var_107: bool) = (var_106 = false)
if var_107 then
    (failwith "Tensor needs to be at least size 1.")
else
    ()
let (var_111: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(47040000L))
let (var_112: int64) = 0L
method_8((var_95: (uint8 [])), (var_111: (float32 [])), (var_112: int64))
let (var_113: string) = System.IO.Path.Combine("C:\\ML Datasets\\Mnist", "train-labels.idx1-ubyte")
let (var_114: Tuple6) = method_5((var_113: string))
let (var_115: int64) = var_114.mem_0
let (var_116: (uint8 [])) = var_114.mem_1
let (var_117: bool) = (60000L = var_115)
let (var_118: bool) = (var_117 = false)
if var_118 then
    (failwith "Mnist dimensions do not match the expected values.")
else
    ()
let (var_122: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(600000L))
let (var_123: int64) = 0L
method_9((var_116: (uint8 [])), (var_122: (float32 [])), (var_123: int64))
let (var_124: int64) = var_79.LongLength
let (var_125: int64) = (var_124 * 4L)
let (var_126: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_125: int64))
let (var_127: (Union0 ref)) = var_126.mem_0
let (var_128: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_127: (Union0 ref)))
var_1.CopyToDevice(var_128, var_79)
let (var_129: int64) = var_90.LongLength
let (var_130: int64) = (var_129 * 4L)
let (var_131: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_130: int64))
let (var_132: (Union0 ref)) = var_131.mem_0
let (var_133: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_132: (Union0 ref)))
var_1.CopyToDevice(var_133, var_90)
let (var_134: int64) = var_111.LongLength
let (var_135: int64) = (var_134 * 4L)
let (var_136: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_135: int64))
let (var_137: (Union0 ref)) = var_136.mem_0
let (var_138: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_137: (Union0 ref)))
var_1.CopyToDevice(var_138, var_111)
let (var_139: int64) = var_122.LongLength
let (var_140: int64) = (var_139 * 4L)
let (var_141: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_140: int64))
let (var_142: (Union0 ref)) = var_141.mem_0
let (var_143: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_142: (Union0 ref)))
var_1.CopyToDevice(var_143, var_122)
let (var_144: int64) = 31360L
let (var_145: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_144: int64))
let (var_146: (Union0 ref)) = var_145.mem_0
let (var_147: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_146: (Union0 ref)))
let (var_148: ManagedCuda.BasicTypes.SizeT) = var_147.Pointer
let (var_149: uint64) = uint64 var_148
let (var_150: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_149)
let (var_151: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_150)
let (var_152: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(7840L)
var_53.GenerateNormal32(var_151, var_152, 0.000000f, 0.050189f)
let (var_153: int64) = 31360L
let (var_154: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_153: int64))
let (var_155: (Union0 ref)) = var_154.mem_0
let (var_156: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_155: (Union0 ref)))
let (var_157: ManagedCuda.BasicTypes.SizeT) = var_156.Pointer
let (var_158: uint64) = uint64 var_157
let (var_159: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_158)
let (var_160: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_159)
let (var_161: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_162: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(31360L)
var_1.ClearMemoryAsync(var_160, 0uy, var_162, var_161)
let (var_163: int64) = 40L
let (var_164: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_163: int64))
let (var_165: (Union0 ref)) = var_164.mem_0
let (var_166: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_165: (Union0 ref)))
let (var_167: ManagedCuda.BasicTypes.SizeT) = var_166.Pointer
let (var_168: uint64) = uint64 var_167
let (var_169: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_168)
let (var_170: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_169)
let (var_171: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_172: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(40L)
var_1.ClearMemoryAsync(var_170, 0uy, var_172, var_171)
let (var_173: int64) = 40L
let (var_174: EnvStack2) = method_10((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_173: int64))
let (var_175: (Union0 ref)) = var_174.mem_0
let (var_176: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_175: (Union0 ref)))
let (var_177: ManagedCuda.BasicTypes.SizeT) = var_176.Pointer
let (var_178: uint64) = uint64 var_177
let (var_179: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_178)
let (var_180: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_179)
let (var_181: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_182: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(40L)
var_1.ClearMemoryAsync(var_180, 0uy, var_182, var_181)
let (var_183: int64) = 0L
method_13((var_1: ManagedCuda.CudaContext), (var_51: ManagedCuda.CudaStream), (var_49: uint64), (var_50: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_32: ManagedCuda.BasicTypes.CUmodule), (var_174: EnvStack2), (var_164: EnvStack2), (var_58: ManagedCuda.CudaBlas.CudaBlasHandle), (var_154: EnvStack2), (var_145: EnvStack2), (var_126: EnvStack2), (var_131: EnvStack2), (var_136: EnvStack2), (var_141: EnvStack2), (var_183: int64))
var_175 := Union0Case1
var_165 := Union0Case1
var_155 := Union0Case1
var_146 := Union0Case1
var_57.Dispose()
var_53.Dispose()
var_51.Dispose()
let (var_184: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_46: (Union0 ref)))
var_1.FreeMemory(var_184)
var_46 := Union0Case1
var_1.Dispose()

