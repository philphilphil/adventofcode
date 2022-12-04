use std::collections::HashSet;

use crate::base::{Problem, ProblemData};

pub struct Day4 {}

impl Problem for Day4 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let mut fully_contained_in_other = 0;

        for line in problem_data.input.lines() {
            let ranges = line_to_ranges(line);

            let p1_in_p2 = ranges.0.intersection(&ranges.1);
            if p1_in_p2.count() == ranges.0.len() {
                fully_contained_in_other += 1;
                continue;
            }

            let p2_in_p1 = ranges.1.intersection(&ranges.0);
            if p2_in_p1.count() == ranges.1.len() {
                fully_contained_in_other += 1;
            }
        }

        fully_contained_in_other.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        let mut any_on_other = 0;

        for line in problem_data.input.lines() {
            let ranges = line_to_ranges(line);

            if ranges.0.intersection(&ranges.1).count() > 0 {
                any_on_other += 1;
            }
        }

        any_on_other.to_string()
    }
}

fn line_to_ranges(line: &str) -> (HashSet<i32>, HashSet<i32>) {
    let mut pairs = line.split(',');

    let mut pair1 = pairs.next().unwrap().split('-');
    let p1_from: i32 = pair1.next().unwrap().parse::<i32>().unwrap();
    let p1_to: i32 = pair1.next().unwrap().parse::<i32>().unwrap();

    let mut pair2 = pairs.next().unwrap().split('-');
    let p2_from: i32 = pair2.next().unwrap().parse::<i32>().unwrap();
    let p2_to: i32 = pair2.next().unwrap().parse::<i32>().unwrap();

    let p1_range = range_to_hashset(p1_from, p1_to);
    let p2_range = range_to_hashset(p2_from, p2_to);
    (p1_range, p2_range)
}

// there has to be a better way todo this
fn range_to_hashset(start: i32, end: i32) -> HashSet<i32> {
    let mut hashset = HashSet::new();
    for i in start..=end {
        hashset.insert(i);
    }
    hashset
}
