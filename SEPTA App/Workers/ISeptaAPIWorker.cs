using System;
using SEPTA_App.Models;

namespace SEPTA_App.Workers
{
	public interface ISeptaAPIWorker
	{
        public Task<List<NextToArrive>> GetNextToArriveAsync(string station1, string station2);

        public Task<Direction> GetDirections(string station);

    }
}

