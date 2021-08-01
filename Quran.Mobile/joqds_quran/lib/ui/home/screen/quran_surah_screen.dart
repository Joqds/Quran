import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:persian_number_utility/persian_number_utility.dart';

class QuranSurahScreen extends StatelessWidget {
  const QuranSurahScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<SurahBloc, SurahState>(
      builder: (context, state) {
        if (state is UnSurahState) {
          return const Center(child: CircularProgressIndicator());
        }
        if (state is ErrorSurahState) {
          return Center(
              child: Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text("خطایی رخ داده (${state.errorMessage})"),
              ElevatedButton(
                  onPressed: () {
                    BlocProvider.of<SurahBloc>(context).add(LoadSurahEvent());
                  },
                  child: const Text("تلاش مجدد"))
            ],
          ));
        }
        if (state is InSurahState) {
          return ListView.builder(
            itemCount: state.sovar.length,
            itemBuilder: (context, index) {
              return Card(
                child: TextButton(
                  onPressed: () => goToRead(context, state.sovar[index].id!),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Expanded(
                        flex: 1,
                        child: Text(
                          state.sovar[index].id.toString().toPersianDigit(),
                          textAlign: TextAlign.center,
                          style: Theme.of(context).textTheme.headline6,
                        ),
                      ),
                      Expanded(
                        flex: 5,
                        child: Text(state.sovar[index].name!,
                            textAlign: TextAlign.center,
                            style: Theme.of(context).textTheme.headline6),
                      ),
                      Expanded(
                        flex: 2,
                        child: Text(
                            "آیات: ${state.sovar[index].ayatCount!.toString().toPersianDigit()}",
                            textAlign: TextAlign.right,
                            style: Theme.of(context).textTheme.subtitle1),
                      ),
                    ],
                  ),
                ),
              );
            },
          );
        }
        throw Exception();
      },
    );
  }

  goToRead(BuildContext context, int surahId) {
    BlocProvider.of<NavBloc>(context)
        .add(GoRead(type: ReadViewType.surah, id: surahId, context: context));
  }
}
