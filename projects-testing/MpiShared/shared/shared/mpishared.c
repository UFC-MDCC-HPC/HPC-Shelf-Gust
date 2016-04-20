#include "mpishared.h"
#include "mpi.h"
#include <stdio.h>
int func_c(int argc, char *argv[]){
	mpi_func(argc,&argv);
}
	int mpi_func(int argc, char *argv[]){
	int size, rank;
	//MPI_Init(&argc,&argv);

	MPI_Comm_size(MPI_COMM_WORLD,&size);
	MPI_Comm_rank(MPI_COMM_WORLD,&rank);

	int global_sum=0;
	int local_sum=rank;
	MPI_Reduce(&local_sum, &global_sum, 1, MPI_INT, MPI_SUM, 0, MPI_COMM_WORLD);
	printf("RANK.__C: %d SIZE: %d SOMA: %d\n", rank, size, global_sum);

	//MPI_Finalize();
	return 0;
}

