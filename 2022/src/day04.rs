use crate::base::{Problem, ProblemData};
use anyhow::Result;
use std::collections::HashSet;

pub struct Day4 {}

impl Problem for Day4 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let mut fully_contained_in_other = 0;

        for line in problem_data.input.lines() {
            let ranges = line_to_ranges(line).unwrap();

            if ranges.0.is_subset(&ranges.1) || ranges.1.is_subset(&ranges.0) {
                fully_contained_in_other += 1;
            }
        }

        fully_contained_in_other.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        let mut any_on_other = 0;

        for line in problem_data.input.lines() {
            let ranges = line_to_ranges(line).unwrap();

            if ranges.0.intersection(&ranges.1).count() > 0 {
                any_on_other += 1;
            }
        }

        any_on_other.to_string()
    }
}

fn line_to_ranges(line: &str) -> Result<(HashSet<i32>, HashSet<i32>)> {
    let mut pairs = line.split(',');

    let pair1 = parse_pair(pairs.next().unwrap())?;
    let pair2 = parse_pair(pairs.next().unwrap())?;

    let p1_range = range_to_hashset(pair1.0, pair1.1);
    let p2_range = range_to_hashset(pair2.0, pair2.1);
    Ok((p1_range, p2_range))
}

fn parse_pair(pair: &str) -> Result<(i32, i32)> {
    let mut pair_split = pair.split('-');
    let from: i32 = pair_split.next().unwrap().parse::<i32>()?;
    let to: i32 = pair_split.next().unwrap().parse::<i32>()?;
    Ok((from, to))
}

// there has to be a better way todo this
fn range_to_hashset(start: i32, end: i32) -> HashSet<i32> {
    let mut hashset = HashSet::new();
    for i in start..=end {
        hashset.insert(i);
    }
    hashset
}
