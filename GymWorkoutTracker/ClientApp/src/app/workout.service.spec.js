"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var workout_service_1 = require("./workout.service");
describe('WorkoutService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(workout_service_1.WorkoutService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=workout.service.spec.js.map